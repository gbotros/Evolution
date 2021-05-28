using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Common;
using Evolution.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Evolution.Domain.AnimalAggregate
{

    public class Animal : AggregateRoot
    {
        private const int MinSpeed = 1;
        private const int DefaultSpeed = 500;
        private const int MaxSpeed = 1000;

        private const int MinEnergy = 1;
        // DefaultStepCost = DefaultSpeed * 2;
        // enough for 100 step on default values
        private const int DefaultEnergy = 1_00_000;

        // one food enough for 10 step on default values
        private const int OneFoodToEnergy = 5_000;
        private const double OneEnergyToFood = 1d / 5_000d;

        // equal to 200% DefaultEnergy
        // enough for 200 step on default values
        private const int DefaultFoodStorageCapacity = 20;

        private const uint SpeedMutationAmplitude = 5;

        private readonly GameDays AdulthoodAge = new GameDays(10);


        public Animal(
            Guid id,
            string name,
            Location location,
            IReadOnlyCollection<IPlantFood> food,
            Guid? parentId,
            int? speed,
            IGameCalender calender,
            ILogger<Animal> logger) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ApplicationException("Name can't be empty");

            Id = id;
            Name = name;
            Location = location;
            ParentId = parentId;
            Food = food ?? new List<IPlantFood>();

            IsAlive = true;
            CreationTime = calender.Now;
            Energy = DefaultEnergy;
            FoodStorageCapacity = DefaultFoodStorageCapacity;
            Speed = speed ?? DefaultSpeed;

            Calender = calender;
            Logger = logger;
        }

        public GameDays Age => new GameDays(Calender.Now - CreationTime);

        public DateTime CreationTime { get; private set; }
        public DateTime? DeathTime { get; private set; }

        public bool IsAlive { get; private set; }
        public string Name { get; private set; }
        public Guid? ParentId { get; private set; }
        public int Weight { get; private set; }
        public Location Location { get; private set; }
        public IReadOnlyCollection<IPlantFood> Food { get; private set; }

        protected ILogger<Animal> Logger { get; }

        protected IGameCalender Calender { get; }

        public int ChildrenCount { get; private set; }

        public int Speed { get; }
        public int Steps { get; private set; }

        public int StoredFood { get; private set; }
        public int FoodStorageCapacity { get; private set; }

        private int energy;
        public int Energy
        {
            get
            {
                return energy;
            }

            private set
            {
                energy = value > 0 ? value : 0;
                if (Energy < MinEnergy) Die();
            }
        }

        private int StepCost => Speed * 2; // Energy unit

        public void Act()
        {
            if (!IsAlive)
            {
                Logger.LogDebug($"Dead Animal {Name} Can not Act");
                return;
            }

            Logger.LogDebug($"Animal {Name} started Act");
            SatisfyEssinsialNeeds();
            Digst();
        }

        public int EatInto(int desiredAmount)
        {
            throw new NotImplementedException();
        }

        public bool IsEatableBy(Type otherType)
        {
            return false;
        }

        private void Digst()
        {
            if (StoredFood <= 0) return;

            var hungaryThreeShold = DefaultEnergy * 0.75;
            if (Energy > hungaryThreeShold) return;

            var requiredEnergy = DefaultEnergy - Energy;
            var requiredFoodUnits = (int)Math.Ceiling(ConvertEnergyToFood(requiredEnergy));

            var foodUnitsInTransaction = Math.Min(requiredFoodUnits, StoredFood);
            var energyUnitsInTransaction = ConvertFoodToEnergy(foodUnitsInTransaction);

            StoredFood -= foodUnitsInTransaction;
            Energy += energyUnitsInTransaction;
        }

        private bool CanReproduce()
        {
            return IsAdult() && !IsHungry();
        }

        private static double ConvertEnergyToFood(int energy)
        {
            return energy * OneEnergyToFood;
        }

        private static int ConvertFoodToEnergy(int food)
        {
            return food * OneFoodToEnergy;
        }

        private void Die()
        {
            IsAlive = false;
            DeathTime = DateTime.UtcNow;

            Logger.LogDebug($"Creature {Name} Died after {Steps} steps.");
        }

        private void Eat()
        {
            if (!IsFoodAvailable()) return;

            var foods = GetAvailableFood().ToList();

            foreach (var food in foods)
            {
                var neededFood = FoodStorageCapacity - StoredFood;
                var availableFood = Math.Min(neededFood, food.Weight);

                food.EatInto(availableFood);
                StoredFood += availableFood;

                Logger.LogDebug($"Creature {Name} ate {availableFood} Food");
                if (!IsHungry()) break;
            }
        }

        private IEnumerable<IPlantFood> GetAvailableFood()
        {
            return Food;
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
            if (neighboursCount == 0) return null;

            var newLocationIndex = new Random().Next(0, neighboursCount);
            var newLocation = Location.Neighbours.ElementAt(newLocationIndex);


            return newLocation;
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
            return StoredFood < FoodStorageCapacity;
        }

        private void Move()
        {
            var newLocation = GetRandomNeighbor();

            if (newLocation != null)
            {
                Location = newLocation;
                Steps++;
            }

            Energy -= StepCost;
            Logger.LogDebug(
                $"Creature {Name} moved to cell {Location.Name} at step number {Steps} - current Energy = {Energy}");

        }

        private void Reproduce()
        {
            if (!CanReproduce()) return;

            ChildrenCount++;
            // TODO: different animals can have different reproduction cost and can have different number of children
            Energy /= 2; // Son cost 50% of the Parent Energy
            var sonBornEvent = new AnimalBornEvent
            (
                id: Guid.NewGuid(),
                name: $"{Name}:s{ChildrenCount}",
                location: Location,
                parentId: Id,
                speed: GetMutatedSpeed(),
                creationTime: Calender.Now
            );

            RaiseEvent(sonBornEvent);
            Logger.LogDebug($"{Name} gave birth to {sonBornEvent.Name}");
        }

        private void SatisfyEssinsialNeeds()
        {
            if (CanReproduce())
            {
                Reproduce();
            }
            else if (IsHungry() && IsFoodAvailable())
            {
                Eat();
            }
            else
            {
                Move();
            }
        }

    }
}