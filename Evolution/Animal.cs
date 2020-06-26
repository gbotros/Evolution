using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Entities;
using Microsoft.Extensions.Logging;

namespace Evolution
{
    public class Animal : IAnimal
    {
        private const int MaxSpeed = 1000; // 1000 step per game hour
        private const int MaxEnergy = 100000; // on default speed 100K Energy is enough for 100 steps

        public Animal(
            AnimalBlueprint blueprint,
            IAnimalService animalService,
            ILocationFactory locationFactory,
            ILogger logger)
        {
            Id = blueprint.Id;
            Name = blueprint.Name;
            Weight = blueprint.Weight;
            Speed = blueprint.Speed;
            Energy = blueprint.Energy;
            BirthDay = blueprint.BirthDay;
            DeathDay = blueprint.DeathDay;
            IsAlive = blueprint.IsAlive;
            Steps = blueprint.Steps;
            Location = locationFactory.Create(blueprint.Location).Result;

            AnimalService = animalService;
            LocationFactory = locationFactory;
            Logger = logger;
        }

        public IAnimalService AnimalService { get; }
        public int BirthDay { get; }

        public int? DeathDay { get; private set; }

        public Guid Id { get; }

        public bool IsAlive { get; private set; }
        public ILocation Location { get; set; }
        public ILocationFactory LocationFactory { get; }
        public ILogger Logger { get; }

        public string Name { get; }

        public int SonsCount { get; private set; }

        public int Speed { get; }

        public int Steps { get; private set; }
        public int Weight { get; }
        private int Energy { get; set; }

        private int StepCost => Speed * 2; // Energy unit

        public async Task Act()
        {
            Logger.LogDebug($"Animal {Name} started Act");

            await SatisfyMyNeeds();
        }

        public Task<int> EatInto(int neededAmount)
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
                BirthDay = BirthDay,
                DeathDay = DeathDay,
                Energy = Energy,
                Name = Name,
                Weight = Weight,
                Steps = Steps
            };
        }

        private bool CanReproduce()
        {
            return IsAdult() && !IsHungry();
        }

        private static int ConvertEnergyToFood(int energy)
        {
            return (int) Math.Ceiling((decimal) energy / 1000);
        }

        private static int ConvertFoodToEnergy(int food)
        {
            return food * 1000;
        }

        private void Die()
        {
            IsAlive = false;
            DeathDay = null; // TODO: get current day

            Logger.LogDebug($"Creature {Name} Died after {Steps} steps.");
        }

        private async Task Eat()
        {
            if (!IsFoodAvailable()) return;

            var foods = GetAvailablePlants().ToList();

            foreach (var food in foods)
            {
                var neededFood = HowMuchCanIEat();
                var eaten = neededFood; // await food.EatInto(neededFood);

                Energy += ConvertFoodToEnergy(eaten);
                await AnimalService.Update(GetBlueprint());
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
            return ConvertEnergyToFood(MaxEnergy - Energy);
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
            await AnimalService.Update(GetBlueprint());
            Logger.LogDebug(
                $"Creature {Name} moved to cell {Location.Name} at step number {Steps} - current Energy = {Energy}");

            if (Energy <= 0) Die();
        }

        private async Task Reproduce()
        {
            if (!CanReproduce()) return;

            SonsCount++;
            Energy /= 2; // reproduction cost 50% energy
            var son = new AnimalBlueprint
            {
                Id = Guid.NewGuid(),
                Name = $"{Name}:s{SonsCount}",
                BirthDay = 0, // TODO: get current day
                DeathDay = null,
                IsAlive = true,
                Speed = Speed, // TODO: allow mutations here
                Location = Location.Blueprint
            };

            await AnimalService.Update(GetBlueprint());
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
    }
}