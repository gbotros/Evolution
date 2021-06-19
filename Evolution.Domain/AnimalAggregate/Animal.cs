using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Common;
using Evolution.Domain.Events;
using Evolution.Domain.GameSettingsAggregate;

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
            GameSettings settings,
            double minSpeed,
            double maxSpeed,
            double speed,
            uint speedMutationAmplitude,
            double minEnergy,
            double maxEnergy,
            double energy,
            int foodStorageCapacity,
            int oneFoodToEnergy,
            int adulthoodAge,
            int sense
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
            Settings = settings;

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

            Sense = sense;
        }

        protected Animal()
        {

        }

        public DateTime CreationTime { get; private set; }
        public DateTime? DeathTime { get; private set; }

        public bool IsAlive { get; private set; }
        public string Name { get; private set; }
        public Guid? ParentId { get; private set; }
        public int Weight { get; private set; }
        public Location Location { get; private set; }
        public IReadOnlyCollection<IFood> Food { get; internal set; }

        public int ChildrenCount { get; private set; }

        /// <summary>
        /// How many Action can this animal do per sec
        /// </summary>
        public double Speed { get; private set; }
        public double MinSpeed { get; private set; }
        public double MaxSpeed { get; private set; }
        public int Steps { get; private set; }

        public int StoredFood { get; private set; }
        public int FoodStorageCapacity { get; private set; }

        private double energy;
        public double Energy
        {
            get => energy;

            private set
            {
                energy = value > 0 ? value : 0;
                if (Energy < MinEnergy) Die();
            }
        }

        public double MinEnergy { get; private set; }
        public double MaxEnergy { get; private set; }

        public DateTime LastAction { get; private set; }
        public DateTime NextAction { get; private set; }

        public bool IsAdult { get; private set; }

        public int OneFoodToEnergy { get; private set; }

        public uint SpeedMutationAmplitude { get; private set; }

        public int AdulthoodAge { get; private set; }
        public DateTime LastChildAt { get; private set; }

        public int Sense { get; private set; }

        public GameSettings Settings { get; private set; }

        private double StepCost => Speed * 2; // Energy unit

        public void Act(DateTime now)
        {
            if (!IsAlive)
            {
                return;
            }

            UpdateIsAdult(now);
            SatisfyEssentialNeeds(now);
            Digest();

            LastAction = now; // todo: check from unit tests
            NextAction = CalculateNextActionTime(now);
        }

        public double GetAge(DateTime now)
        {
            return (now - CreationTime).TotalSeconds;
        }

        // TODO: Unit tests
        private DateTime CalculateNextActionTime(DateTime now)
        {
            var velocity = 1d / Speed;
            var timeSpan = TimeSpan.FromSeconds(velocity);
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

        private double ConvertEnergyToFood(double energy)
        {
            return energy / OneFoodToEnergy;
        }

        private double ConvertFoodToEnergy(double food)
        {
            return food * OneFoodToEnergy;
        }

        private void Eat()
        {
            if (!IsFoodAvailable()) return;

            var foods = GetAvailableFoodOnMyLocation();

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

        private List<IFood> GetAvailableFoodOnMyLocation()
        {
            return Food.Where(f =>
                f.Location.Row == Location.Row
                && f.Location.Column == Location.Column
                ).ToList();
        }

        private double GetMutatedSpeed()
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            var minMutation = -1 * (int)SpeedMutationAmplitude;
            var maxMutation = (int)SpeedMutationAmplitude;
            var mutation = rand.NextDouble() * rand.Next(minMutation, maxMutation + 1);

            var mutantSpeed = Speed + mutation;

            if (mutantSpeed < MinSpeed) mutantSpeed = MinSpeed;
            if (mutantSpeed > MaxSpeed) mutantSpeed = MaxSpeed;

            return mutantSpeed;
        }

        private Location GetRandomNeighbor()
        {
            var neighbours = Location.GetNeighbours(Settings.WorldSize);
            var neighboursCount = neighbours.Count();
            if (neighboursCount == 0) return Location;

            var newLocationIndex = new Random((int)DateTime.Now.Ticks).Next(0, neighboursCount);
            var newLocation = neighbours.ElementAt(newLocationIndex);

            return newLocation;
        }

        private IFood GetClosestFood()
        {
            IFood closestFood = null;
            var closestDistance = int.MaxValue;
            foreach (var f in Food)
            {
                var deltaRow = Math.Abs(f.Location.Row - Location.Row);
                var deltaColumn = Math.Abs(f.Location.Column - Location.Column);

                var distance = Math.Max(deltaColumn, deltaRow);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestFood = f;
                }
            }

            return closestFood;
        }

        private bool IsFoodAvailable()
        {
            var food = GetAvailableFoodOnMyLocation();
            return food.Sum(f => f.Weight) > 0;
        }

        private bool IsHungry()
        {
            // animal is hungary if he his Stomach  is half empty
            return StoredFood < FoodStorageCapacity * .5;
        }

        private void Move()
        {
            Location newLocation = null;
            if (IsHungry())
            {
                var food = GetClosestFood();
                newLocation = StepInDirection(food?.Location);
            }
            
            newLocation ??= GetRandomNeighbor();

            Location = newLocation;
            Steps++;
            Energy -= StepCost;
        }

        private Location StepInDirection(Location dLocation)
        {
            if (dLocation == null) return null;

            var row = Location.Row;
            var col = Location.Column;

            if (dLocation.Row > row) row++;
            else if (dLocation.Row < row) row--;

            if (dLocation.Column > col) col++;
            else if (dLocation.Column < col) col--;

            return new Location(row, col);
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
                Id,
                new Location(Location.Row, Location.Column),
                Energy,
                GetMutatedSpeed(),
                FoodStorageCapacity,
                OneFoodToEnergy,
                AdulthoodAge,
                Settings.AnimalDefaults.MinSpeed,
                Settings.AnimalDefaults.MaxSpeed,
                Settings.AnimalDefaults.SpeedMutationAmplitude,
                Settings.AnimalDefaults.MinEnergy,
                Settings.AnimalDefaults.MaxEnergy,
                Settings.AnimalDefaults.Sense);

            RaiseEvent(sonBornEvent);
        }

        private void SatisfyEssentialNeeds(DateTime now)
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
                Move();
            }
        }

        private void UpdateIsAdult(DateTime now)
        {
            IsAdult = IsAdult || GetAge(now) >= AdulthoodAge;
        }
    }
}