using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Evolution.Domain
{
    public class Animal : Creature, IAnimal
    {
        private const int MaxSpeed = 1000; // 1000 step per game hour
        private const int MinSpeed = 1;
        private const int MaxEnergy = 500; // on default speed 12K Energy is enough for 200 steps

        private const uint SpeedMutationAmplitude = 5;

        public Animal(
            AnimalBlueprint blueprint,
            ILocation location,
            IGameCalender gameCalender,
            ILogger<Animal> logger) : base(gameCalender)
        {
            Id = blueprint.Id;
            Name = blueprint.Name;
            Weight = blueprint.Weight;
            Speed = blueprint.Speed;
            Energy = blueprint.Energy;
            BirthDate = blueprint.BirthDate;
            DeathDate = blueprint.DeathDate;
            IsAlive = blueprint.IsAlive;
            Steps = blueprint.Steps;
            ChildrenCount = blueprint.ChildrenCount;
            Location = location;
            ParentId = blueprint.ParentId;

            GameCalender = gameCalender;

            Logger = logger;
        }

        public IEnumerable<ICreature> Community { get; private set; }
        public int ChildrenCount { get; private set; }
        public int Speed { get; }

        private int Energy { get; set; }
        private IGameCalender GameCalender { get; }
        public IEnumerable<ILocation> Neighbours { get; }
        private ILogger<Animal> Logger { get; }

        private int StepCost => Speed * 2; // Energy unit
        public int Steps { get; private set; }

        public override bool IsEatable(ICreature other)
        {
            return false;
        }

        public override void Act()
        {
            Logger.LogDebug($"Animal {Name} started Act");

            SatisfyMyNeeds();
        }

        public override void EatInto(int neededAmount)
        {
            throw new NotImplementedException();
        }

        public AnimalBlueprint GetBlueprint()
        {
            return new AnimalBlueprint
            {
                Id = Id,
                IsAlive = IsAlive,
                Speed = Speed,
                BirthDate = BirthDate,
                DeathDate = DeathDate,
                Energy = Energy,
                Name = Name,
                Weight = Weight,
                Steps = Steps,
                UpdatedAt = DateTime.UtcNow,
                ParentId = ParentId,
                ChildrenCount = ChildrenCount
            };
        }

        private bool CanReproduce()
        {
            return IsAdult() && !IsHungry();
        }

        private static int ConvertEnergyToFood(int energy)
        {
            return (int)Math.Ceiling((decimal)energy / 100);
        }

        private static int ConvertFoodToEnergy(int food)
        {
            return food * 100;
        }

        private void Die()
        {
            IsAlive = false;
            DeathDate = DateTime.UtcNow;

            Logger.LogDebug($"Creature {Name} Died after {Steps} steps.");
        }

        private void Eat()
        {
            if (!IsFoodAvailable()) return;

            var foods = GetAvailableFood().ToList();

            foreach (var food in foods)
            {
                var neededFood = HowMuchCanIEat();
                var availableFood = Math.Min(neededFood, food.Weight);
                food.EatInto(availableFood);

                Energy += ConvertFoodToEnergy(availableFood);
                Logger.LogDebug($"Creature {Name} ate {availableFood} Food - current Energy {Energy}");
                if (!IsHungry()) break;
            }
        }

        private IEnumerable<ICreature> GetAvailableFood()
        {
            return Location.Community.Where(c => c.IsEatable(this));
        }

        private IEnumerable<ILocation> GetNeighbors()
        {
            return Location.Neighbours;
        }

        private int HowMuchCanIEat()
        {
            var neededEnergy = MaxEnergy - Energy;
            return ConvertEnergyToFood(neededEnergy);
        }

        private bool IsAdult()
        {
            return Steps > 5; // TODO: Determine is adult from age
        }

        private bool IsFoodAvailable()
        {
            var food = GetAvailableFood().ToList();
            return food.Sum(f => f.Weight) > 0;
        }

        private bool IsHungry()
        {
            return Energy < MaxEnergy / 2;
        }

        private void Move()
        {
            var neighboursCount = GetNeighbors().Count();
            if (neighboursCount == 0) return;

            var newLocationIndex = new Random().Next(0, neighboursCount);
            var newLocation = Location.Neighbours.ElementAt(newLocationIndex);

            Location = newLocation;
            Location.Move(this, newLocation);

            Steps++;
            Energy -= StepCost;
            Logger.LogDebug(
                $"Creature {Name} moved to cell {Location.Name} at step number {Steps} - current Energy = {Energy}");

            if (Energy <= 0) Die();
        }

        private void Reproduce()
        {
            if (!CanReproduce()) return;

            ChildrenCount++;
            // TODO: different animals can have different reproduction cost
            Energy /= 2; // Son gets 50% of the Parent Energy
            var son = new AnimalBlueprint
            {
                Id = Guid.NewGuid(),
                Name = $"{Name}:s{ChildrenCount}",
                Energy = Energy, // Son gets 50% of the Parent Energy
                BirthDate = DateTime.UtcNow,
                DeathDate = null,
                IsAlive = true,
                Speed = GetMutatedSpeed(),
                Location = Location,
                ParentId = Id
            };

            // TODO: raise event add son animal

            Logger.LogDebug($"{Name} gave birth to {son.Name}");
        }

        private void SatisfyMyNeeds()
        {
            if (CanReproduce()) Reproduce();
            if (IsHungry() && IsFoodAvailable()) Eat();
            else  Move();
        }

        private int GetMutatedSpeed()
        {
            var minMutation = -1 * (int)SpeedMutationAmplitude;
            var maxMutation = (int)SpeedMutationAmplitude + 1;
            var mutation = new Random().Next(minMutation, maxMutation);

            var mutantSpeed = Speed + mutation;

            if (mutantSpeed < MinSpeed) mutantSpeed = MinSpeed;
            if (mutantSpeed > MaxSpeed) mutantSpeed = MaxSpeed;

            return mutantSpeed;
        }

    }
}