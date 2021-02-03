using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Evolution.Domain
{
    public class Animal : Creature
    {
        private const int MaxSpeed = 1000;
        private const int MinSpeed = 1;
        private const int DefaultSpeed = 500;
        private const int MaxEnergy = 500;

        private const uint SpeedMutationAmplitude = 5;

        private readonly GameDays AdulthoodAge = new GameDays(10);

        public Animal(
            Guid id,
            string name,
            Location location,
            IReadOnlyCollection<Creature> creaturesWithinVisionLimit,
            ILogger<Animal> logger) : this(id, name, location, null, null, DefaultSpeed, logger)
        {
        }

        public Animal(
            Guid id,
            string name,
            Location location,
            IReadOnlyCollection<Creature> creaturesWithinVisionLimit,
            Guid? parentId,
            int speed,
            ILogger<Animal> logger) : base(id, name, location, creaturesWithinVisionLimit, parentId, logger)
        {
            Energy = MaxEnergy;
            Speed = speed;
        }

        public int ChildrenCount { get; private set; }

        public int Speed { get; }
        public int Steps { get; private set; }

        public int Energy { get; private set; }

        private int StepCost => Speed * 2; // Energy unit

        public override void Act()
        {
            Logger.LogDebug($"Animal {Name} started Act");

            SatisfyMyNeeds();
        }

        public override void EatInto(int neededAmount)
        {
            throw new NotImplementedException();
        }

        public override bool IsEatableBy(Type otherType)
        {
            return false;
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

        private IEnumerable<Creature> GetAvailableFood()
        {
            return CreaturesWithinVisionLimit.Where(c => c.Location == Location && c.IsEatableBy(GetType()));
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

        private Location GetRandomNeighbor()
        {
            var neighbours = Location.Neighbours;
            var neighboursCount = neighbours.Count();
            if (neighboursCount == 0) return Location;

            var newLocationIndex = new Random().Next(0, neighboursCount);
            var newLocation = Location.Neighbours.ElementAt(newLocationIndex);


            return newLocation;
        }

        private int HowMuchCanIEat()
        {
            var neededEnergy = MaxEnergy - Energy;
            return ConvertEnergyToFood(neededEnergy);
        }

        private bool IsAdult()
        {
            return Age >= AdulthoodAge;
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
            var newLocation = GetRandomNeighbor();

            Location = newLocation;

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
                         // var sonBlueprint = new AnimalBlueprint
                         // {
                         //     Id = Guid.NewGuid(),
                         //     Name = $"{Name}:s{ChildrenCount}",
                         //     Energy = Energy, // Son gets 50% of the Parent Energy
                         //     BirthDate = DateTime.UtcNow,
                         //     DeathDate = null,
                         //     IsAlive = true,
                         //     Speed = GetMutatedSpeed(),
                         //     Location = Location,
                         //     ParentId = Id
                         // };

            // TODO: Domain even gave birth
            // Logger.LogDebug($"{Name} gave birth to {son.Name}");
        }

        private void SatisfyMyNeeds()
        {
            if (CanReproduce()) Reproduce();
            if (IsHungry() && IsFoodAvailable()) Eat();
            else Move();
        }

    }
}