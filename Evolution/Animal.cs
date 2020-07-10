using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Entities;
using Microsoft.Extensions.Logging;

namespace Evolution
{
    public class Animal : Creature, IAnimal
    {
        private const int MaxSpeed = 1000; // 1000 step per game hour
        private const int MinSpeed = 1;
        private const int MaxEnergy = 500; // on default speed 12K Energy is enough for 200 steps

        private const uint SpeedMutationAmplitude = 5;

        public Animal(
            AnimalBlueprint blueprint,
            IAnimalService animalService,
            IPlantFactory plantFactory,
            ILocationFactory locationFactory,
            IGameCalender gameCalender,
            ILogger logger) : base(gameCalender)
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
            Location = locationFactory.Create(blueprint.Location).Result;
            Children = new List<AnimalBlueprint>();

            AnimalService = animalService;
            PlantFactory = plantFactory;
            LocationFactory = locationFactory;
            GameCalender = gameCalender;
            Logger = logger;
        }

        public IEnumerable<AnimalBlueprint> Children { get; }
        public int ChildrenCount { get; private set; }
        public int Speed { get; }

        private IAnimalService AnimalService { get; }
        private int Energy { get; set; }
        private IGameCalender GameCalender { get; }
        private ILocationFactory LocationFactory { get; }
        private ILogger Logger { get; }
        private IPlantFactory PlantFactory { get; }

        private int StepCost => Speed * 2; // Energy unit
        private int Steps { get; set; }

        public override async Task Act()
        {
            Logger.LogDebug($"Animal {Name} started Act");

            await SatisfyMyNeeds();

            await AnimalService.Update(GetBlueprint());
        }

        public override Task<int> EatInto(int neededAmount)
        {
            throw new NotImplementedException();
        }

        public int Fight(int neededAmount)
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
                Location = Location.Blueprint,
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

        private async Task Eat()
        {
            if (!IsFoodAvailable()) return;

            var foods = GetAvailablePlants().ToList();

            foreach (var food in foods)
            {
                var neededFood = HowMuchCanIEat();
                var plant = PlantFactory.Create(food);
                var eaten = await plant.EatInto(neededFood);

                Energy += ConvertFoodToEnergy(eaten);
                Logger.LogDebug($"Creature {Name} ate {eaten} Food - current Energy {Energy}");
                if (!IsHungry()) break;
            }
        }

        private IEnumerable<PlantBlueprint> GetAvailablePlants()
        {
            return Location.Plants ?? new List<PlantBlueprint>();
        }

        private IEnumerable<LocationBlueprint> GetNeighbors()
        {
            return Location.Neighbours ?? new List<LocationBlueprint>();
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
            var food = GetAvailablePlants().ToList();
            return food.Sum(f => f.Weight) > 0;
        }

        private bool IsHungry()
        {
            return Energy < MaxEnergy / 2;
        }

        private async Task Move()
        {
            var neighboursCount = GetNeighbors().Count();
            if (neighboursCount == 0) return;

            var newLocationIndex = new Random().Next(0, neighboursCount);
            var newLocationBlueprint = Location.Neighbours.ElementAt(newLocationIndex);

            Location = await LocationFactory.CreateEmpty(newLocationBlueprint);

            Steps++;
            Energy -= StepCost;
            Logger.LogDebug(
                $"Creature {Name} moved to cell {Location.Name} at step number {Steps} - current Energy = {Energy}");

            if (Energy <= 0) Die();
        }

        private async Task Reproduce()
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
                Location = Location.Blueprint,
                ParentId = Id
            };

            await AnimalService.Add(son);
            Logger.LogDebug($"{Name} gave birth to {son.Name}");
        }

        private async Task SatisfyMyNeeds()
        {
            if (CanReproduce()) await Reproduce();

            if (IsHungry() && IsFoodAvailable())
                await Eat();
            else
                await Move();
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