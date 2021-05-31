using System;
using Evolution.Domain.Common;

namespace Evolution.Domain.Events
{
    public sealed class AnimalBornEvent : IDomainEvent
    {
        public AnimalBornEvent(
            string name,
            Location location,
            Guid parentId,
            int speed
            )
        {
            Name = name;
            Location = location;
            ParentId = parentId;
            Speed = speed;
        }

        public string Name { get; }
        public Location Location { get; }
        public Guid ParentId { get; }
        public int Speed { get; }
    }
}
