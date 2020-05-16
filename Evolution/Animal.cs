using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Abstractions;
using Evolution.Blueprints;
using Microsoft.Extensions.Logging;

namespace Evolution
{
    public class Animal : IAnimal
    {
        private const int MaxSpeed = 1000; // 1000 step per second
        private const int MaxEnergy = 100000; // on default speed 100K Energy is enough for 100 steps

        public Animal(AnimalBlueprint blueprint, IPlantService plantService, IAnimalObserver animalObserver,
            ILocationService locationService,
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
            Location = locationService.GetLocation(blueprint.Location);

            Blueprint = blueprint;
            PlantService = plantService;
            AnimalObserver = animalObserver;
            LocationService = locationService;
            Logger = logger;
        }

        public IAnimalObserver AnimalObserver { get; }
        public int BirthDay { get; }

        public AnimalBlueprint Blueprint { get; }
        public int? DeathDay { get; private set; }

        public Guid Id { get; }

        public bool IsAlive { get; private set; }
        public ILocation Location { get; set; }
        public ILocationService LocationService { get; }
        public ILogger Logger { get; }

        public string Name { get; }
        public IPlantService PlantService { get; }

        public int SonsCount { get; private set; }

        public int Speed { get; }

        public int Steps { get; private set; }
        public int Weight { get; }
        private int Energy { get; set; }

        private int StepCost => Speed * 2; // Energy unit

        private int WaitTime => MaxSpeed / Speed + 500;

        public void Act()
        {
            Logger.LogDebug($"Animal {Name} started Act");

            while (IsAlive) SatisfyMyNeeds();
        }

        public int Fight(int neededAmount)
        {
            throw new NotImplementedException();
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

        private void Eat()
        {
            if (!IsFoodAvailable()) return;

            var foods = GetAvailableFood().ToList();

            foreach (var food in foods)
            {
                var neededFood = HowMuchCanIEat();
                var eaten = PlantService.EatInto(food.Id, neededFood);

                Energy += ConvertFoodToEnergy(eaten);
                AnimalObserver.OnEat(this, food);
                Logger.LogDebug($"Creature {Name} ate {eaten} Food - current Energy {Energy}");
                if (!IsHungry()) break;
            }
        }

        private IEnumerable<IPlant> GetAvailableFood()
        {
            return Location.Plants;
        }

        private int HowMuchCanIEat()
        {
            return ConvertEnergyToFood(MaxEnergy - Energy);
        }

        private bool IsAdult()
        {
            return Steps > 5;
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
            var neighboursCount = Location.Neighbours.Count();
            if (neighboursCount == 0) return;

            var newLocationIndex = new Random().Next(0, neighboursCount);
            var newLocation = Location.Neighbours.ElementAt(newLocationIndex);

            Location = newLocation;

            Steps++;
            Energy -= StepCost;
            AnimalObserver.OnMove(this);
            Logger.LogDebug(
                $"Creature {Name} moved to cell {Location.Name} at step number {Steps} - current Energy = {Energy}");

            if (Energy <= 0) Die();
        }

        private void Reproduce()
        {
            if (!CanReproduce()) return;

            SonsCount++;
            Energy /= 2; // reproduction cost 50% energy
            var sonBluePrint = new AnimalBlueprint
            {
                Id = Guid.NewGuid(),
                Name = $"{Name}:s{SonsCount}",
                BirthDay = 0, // TODO: get current day
                DeathDay = null,
                IsAlive = true,
                Speed = Speed, // allow mutations here
                Location = Location.ToBlueprint()
            };

            var son = new Animal(sonBluePrint, PlantService, AnimalObserver, LocationService, Logger);
            AnimalObserver.OnReproduce(this, son);
            Logger.LogDebug($"{Name} gave birth to {son.Name}");
        }

        private void SatisfyMyNeeds()
        {
            if (CanReproduce()) Reproduce();

            if (IsHungry() && IsFoodAvailable()) Eat();
            else Move();
        }
    }

    public interface ILocationService
    {
        ILocation GetLocation(LocationBlueprint blueprint);
    }
}