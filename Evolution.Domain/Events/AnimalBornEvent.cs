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
            int energy,
            int speed,
            int foodStorageCapacity,
            int oneFoodToEnergy,
            int adulthoodAge, 
            int minSpeed, 
            int maxSpeed, 
            uint speedMutationAmplitude, 
            int minEnergy, 
            int maxEnergy)
        {
            Name = name;
            Location = location;
            ParentId = parentId;
            Energy = energy;
            Speed = speed;
            FoodStorageCapacity = foodStorageCapacity;
            OneFoodToEnergy = oneFoodToEnergy;
            AdulthoodAge = adulthoodAge;
            MinSpeed = minSpeed;
            MaxSpeed = maxSpeed;
            SpeedMutationAmplitude = speedMutationAmplitude;
            MinEnergy = minEnergy;
            MaxEnergy = maxEnergy;
        }

        public string Name { get; }
        public Location Location { get; }
        public Guid ParentId { get; }
        public int Speed { get; }
        public int FoodStorageCapacity { get; }
        public int Energy { get; }
        public int OneFoodToEnergy { get; }
        public int AdulthoodAge { get; }
        public int MinSpeed { get; }
        public int MaxSpeed { get; }
        public uint SpeedMutationAmplitude { get; }
        public int MinEnergy { get; }
        public int MaxEnergy { get; }
    }
}
