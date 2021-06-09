using System;
using Evolution.Domain.Common;

namespace Evolution.Domain.Events
{
    public sealed class AnimalBornEvent : IDomainEvent
    {
        public AnimalBornEvent(string name,
            Location location,
            int energy,
            int speed,
            int foodStorageCapacity,
            Guid parentId)
        {
            Name = name;
            Location = location;
            ParentId = parentId;
            Energy = energy;
            FoodStorageCapacity = foodStorageCapacity;
            Speed = speed;
        }

        public string Name { get; }
        public Location Location { get; }
        public Guid ParentId { get; }
        public int Speed { get; }
        public int FoodStorageCapacity { get; }
        public int Energy { get; }
    }
}
