using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Common;
using Evolution.Domain.Events;

namespace Evolution.Domain.AnimalAggregate
{
    public class Animal : AggregateRoot
    {

        public Animal(
            Guid id,
            Guid? parentId,
            string name,
            Location location,
            DateTime creationTime,
            bool isAlive,
            int minSpeed,
            int maxSpeed,
            int speed,
            uint speedMutationAmplitude,
            int minEnergy,
            int maxEnergy,
            int energy,
            int foodStorageCapacity,
            int oneFoodToEnergy,
            int adulthoodAge
        ) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ApplicationException("Name can't be empty");
            if (speed <= 0) throw new ApplicationException("Speed can not be less than one action per game hour");

            Id = id;
            ParentId = parentId;
            Name = name;
            Location = location;
            CreationTime = creationTime;
            IsAlive = isAlive;

            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
            Speed = speed;
            SpeedMutationAmplitude = speedMutationAmplitude;

            MinEnergy = minEnergy;
            MaxEnergy = maxEnergy;
            Energy = energy;

            FoodStorageCapacity = foodStorageCapacity;
            OneFoodToEnergy = oneFoodToEnergy;
            AdulthoodAge = adulthoodAge;

            NextAction = creationTime;
        }

        protected Animal()
        {

        }

        public DateTime CreationTime { get; }
        public DateTime? DeathTime { get; private set; }

        public bool IsAlive { get; private set; }
        public string Name { get; private set; }
        public Guid? ParentId { get; }
        public int Weight { get; private set; }
        public Location Location { get; private set; }
        public IReadOnlyCollection<IPlantFood> Food { get; internal set; }

        public int ChildrenCount { get; private set; }

        /// <summary>
        /// How many Action can this animal do in one hour Example Speed => 3600 equal to one action per sec
        /// </summary>
        public int Speed { get; private set; }
        public int MinSpeed { get; private set; }
        public int MaxSpeed { get; private set; }
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

        public int MinEnergy { get; private set; }
        public int MaxEnergy { get; private set; }

        public DateTime LastAction { get; private set; }
        public DateTime NextAction { get; private set; }

        public bool IsAdult { get; private set; }

        public int OneFoodToEnergy { get; private set; }

        public uint SpeedMutationAmplitude { get; private set; }

        public int AdulthoodAge { get; private set; }
        public DateTime LastChildAt { get; private set; }

        private int StepCost => Speed * 2; // Energy unit

        public void Act(DateTime now, WorldSize worldSize)
        {
            if (!IsAlive)
            {
                return;
            }

            UpdateIsAdult(now);
            SatisfyEssentialNeeds(now, worldSize);
            Digest();

            LastAction = now; // todo: check from unit tests
            NextAction = CalculateNextActionTime(now);
        }

        public double GetAge(DateTime now)
        {
            return (now - CreationTime).TotalSeconds;
        }

        private DateTime CalculateNextActionTime(DateTime now)
        {
            var velocity = 1d / Speed;
            var timeSpan = TimeSpan.FromHours(velocity);
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
            if (!IsAdult) return false;
            if (IsHungry()) return false;

            var afterBirthBreak = 30;//sec
            if ((now - LastChildAt).TotalSeconds <= afterBirthBreak) return false;

            return true;
        }

        private double ConvertEnergyToFood(int energy)
        {
            return energy / (double)OneFoodToEnergy;
        }

        private int ConvertFoodToEnergy(int food)
        {
            return food * OneFoodToEnergy;
        }

        private void Eat()
        {
            if (!IsFoodAvailable()) return;

            var foods = GetAvailableFood().ToList();

            foreach (var food in foods)
            {
                // TODO: Convert to DomainEvent
                var neededFood = FoodStorageCapacity - StoredFood;
                var availableFood = Math.Min(neededFood, food.Weight);

                food.EatInto(availableFood);
                StoredFood += availableFood;

                Weight += availableFood;
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
            var maxMutation = (int)SpeedMutationAmplitude;
            var mutation = new Random().Next(minMutation, maxMutation + 1);

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
            LastChildAt = now;
            // TODO: different animals can have different reproduction cost and can have different number of children
            Energy /= 2; // Son cost 50% of the Parent Energy
            var sonBornEvent = new AnimalBornEvent
            (
                $"{Name}:s{ChildrenCount}",
                new Location(Location.Row, Location.Column),
                Energy,
                GetMutatedSpeed(),
                FoodStorageCapacity, // TODO: allow mutations
                Id);

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

        private void UpdateIsAdult(DateTime now)
        {
            IsAdult = IsAdult || GetAge(now) >= AdulthoodAge;
        }
    }
}