using System;
using Evolution.Domain.Common;

namespace Evolution.Domain.Events
{
    public sealed class AnimalBornEvent : IDomainEvent
    {
        public AnimalBornEvent(
            Guid id,
            string name,
            Location location,
            Guid parentId,
            int speed,
            DateTime creationTime
            )
        {
            Id = id;
            Name = name;
            Location = location;
            ParentId = parentId;
            Speed = speed;
            CreationTime = creationTime;
        }

        public Guid Id { get; }
        public string Name { get; }
        public Location Location { get; }
        public Guid ParentId { get; }
        public int Speed { get; }
        public DateTime CreationTime { get; }
    }
}
