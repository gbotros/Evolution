using System;
using Evolution.Domain.Common;

namespace Evolution.Domain.Events
{
    public sealed class AnimalBornEvent : IDomainEvent
    {
        public AnimalBornEvent(
            string name,
            Guid parentId,
            Location location,
            double energy,
            double speed,
            int minFoodStorageCapacity,
            int maxFoodStorageCapacity,
            int foodStorageCapacity,
            int oneFoodToEnergy,
            int adulthoodAge,
            double minSpeed,
            double maxSpeed, 
            uint speedMutationAmplitude,
            double minEnergy,
            double maxEnergy,
            int sense,
            int minSense, 
            int maxSense,
            uint senseMutationAmplitude)
        {
            Name = name;
            Location = location;
            ParentId = parentId;
            Energy = energy;
            Speed = speed;
            MinFoodStorageCapacity = minFoodStorageCapacity;
            MaxFoodStorageCapacity = maxFoodStorageCapacity;
            FoodStorageCapacity = foodStorageCapacity;
            OneFoodToEnergy = oneFoodToEnergy;
            AdulthoodAge = adulthoodAge;
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
            SpeedMutationAmplitude = speedMutationAmplitude;
            MinEnergy = minEnergy;
            MaxEnergy = maxEnergy;
            Sense = sense;
            MinSense = minSense;
            MaxSense = maxSense;
            SenseMutationAmplitude = senseMutationAmplitude;
        }

        public string Name { get; }
        public Location Location { get; }
        public Guid ParentId { get; }
        public double Speed { get; }
        public int MinFoodStorageCapacity { get; set; }
        public int MaxFoodStorageCapacity { get; set; }
        public int FoodStorageCapacity { get; }
        public double Energy { get; }
        public int OneFoodToEnergy { get; }
        public int AdulthoodAge { get; }
        public double MinSpeed { get; }
        public double MaxSpeed { get; }
        public uint SpeedMutationAmplitude { get; }
        public double MinEnergy { get; }
        public double MaxEnergy { get; }
        public int Sense { get; }
        public int MinSense { get; }
        public int MaxSense { get; }
        public uint SenseMutationAmplitude { get; }
    }
}
