using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Common;
using Evolution.Domain.Events;

namespace Evolution.Domain.AnimalAggregate
{
    public class Animal : AggregateRoot
    {
        private const int MinSpeed = 1;
        private const int MaxSpeed = 1000;

        private const int MinEnergy = 1;
        private const int MaxEnergy = 1_00_000;

        // one food enough for 10 step on default values
        private const int OneFoodToEnergy = 5_000;
        private const double OneEnergyToFood = 1d / 5_000d;

        private const uint SpeedMutationAmplitude = 5;

        private readonly GameDays AdulthoodAge = new GameDays(10);


        public Animal(
            Guid id,
            string name,
            Location location,
            DateTime creationTime,
            bool isAlive,
            int energy,
            int foodStorageCapacity,
            int speed,
            Guid? parentId) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ApplicationException("Name can't be empty");
            if (speed <= 0) throw new ApplicationException("Speed can not be less than one action per game hour");

            Id = id;
            Name = name;
            Location = location;

            //Food = food ?? new List<IPlantFood>();
            IsAlive = isAlive;
            Energy = energy;
            FoodStorageCapacity = foodStorageCapacity;

            CreationTime = creationTime;
            Speed = speed;
            ParentId = parentId;
        }

        protected Animal()
        {

        }

        public GameDays GetAgeInDays(DateTime now)
        {
            return new GameDays(now - CreationTime);
        }

        public DateTime CreationTime { get; private set; }
        public DateTime? DeathTime { get; private set; }

        public bool IsAlive { get; private set; }
        public string Name { get; private set; }
        public Guid? ParentId { get; private set; }
        public int Weight { get; private set; }
        public Location Location { get; private set; }
        public IReadOnlyCollection<IPlantFood> Food { get; internal set; }

        public int ChildrenCount { get; private set; }

        /// <summary>
        /// How many Action can this animal do in one game hour
        /// </summary>
        public int Speed { get; }
        public int Steps { get; private set; }

        public int StoredFood { get; private set; }
        public int FoodStorageCapacity { get; private set; }

        private int energy;
        public int Energy
        {
            get => energy;

            private set
            {
                energy = value > 0 ? value : 0;
                if (Energy < MinEnergy) Die();
            }
        }

        public DateTime LastAction { get; set; }
        public DateTime NextAction { get; set; }

        private int StepCost => Speed * 2; // Energy unit

        public void Act(DateTime now, WorldSize worldSize)
        {
            if (!IsAlive)
            {
                return;
            }

            SatisfyEssentialNeeds(now, worldSize);
            Digest();

            LastAction = now; // todo: check from unit tests
            NextAction = CalculateNextActionTime(now);
        }

        private DateTime CalculateNextActionTime(DateTime now)
        {
            var velocity = 1d / Speed;
            var timeSpan = GameDays.FromGameHours(velocity).TimeSpan;
            var nextActionTime = now + timeSpan;
            return nextActionTime;
        }

        public int EatInto(int desiredAmount)
        {
            throw new NotImplementedException();
        }

        public bool IsEatableBy(Type otherType)
        {
            return false;
        }

        public void Die()
        {
            IsAlive = false;
            DeathTime = DateTime.UtcNow;
        }

        private void Digest()
        {
            if (StoredFood <= 0) return;
            if (Energy >= MaxEnergy) return;

            var requiredEnergy = MaxEnergy - Energy;
            var requiredFoodUnits = (int)Math.Ceiling(ConvertEnergyToFood(requiredEnergy));

            var foodUnitsInTransaction = Math.Min(requiredFoodUnits, StoredFood);
            var energyUnitsInTransaction = ConvertFoodToEnergy(foodUnitsInTransaction);

            StoredFood -= foodUnitsInTransaction;
            Energy += energyUnitsInTransaction;
        }

        private bool CanReproduce(DateTime now)
        {
            return IsAdult(now) && !IsHungry();
        }

        private static double ConvertEnergyToFood(int energy)
        {
            return energy * OneEnergyToFood;
        }

        private static int ConvertFoodToEnergy(int food)
        {
            return food * OneFoodToEnergy;
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

                if (!IsHungry()) break;
            }
        }

        private IEnumerable<IPlantFood> GetAvailableFood()
        {
            return Food ?? new List<IPlantFood>();
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

        private Location GetRandomNeighbor(WorldSize worldSize)
        {
            var neighbours = Location.GetNeighbours(worldSize);
            var neighboursCount = neighbours.Count();
            if (neighboursCount == 0) return null;

            var newLocationIndex = new Random().Next(0, neighboursCount);
            var newLocation = neighbours.ElementAt(newLocationIndex);


            return newLocation;
        }

        private bool IsAdult(DateTime now)
        {
            var age = GetAgeInDays(now);
            return age >= AdulthoodAge;
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

        private void Move(WorldSize worldSize)
        {
            var newLocation = GetRandomNeighbor(worldSize);

            if (newLocation != null)
            {
                Location = newLocation;
                Steps++;
            }

            Energy -= StepCost;
        }

        private void Reproduce(DateTime now)
        {
            if (!CanReproduce(now)) return;

            ChildrenCount++;
            // TODO: different animals can have different reproduction cost and can have different number of children
            Energy /= 2; // Son cost 50% of the Parent Energy
            var sonBornEvent = new AnimalBornEvent
            (
                name: $"{Name}:s{ChildrenCount}",
                location: Location,
                parentId: Id,
                speed: GetMutatedSpeed()
            );

            RaiseEvent(sonBornEvent);
        }

        private void SatisfyEssentialNeeds(DateTime now, WorldSize worldSize)
        {
            if (CanReproduce(now))
            {
                Reproduce(now);
            }
            else if (IsHungry() && IsFoodAvailable())
            {
                Eat();
            }
            else
            {
                Move(worldSize);
            }
        }

    }
}