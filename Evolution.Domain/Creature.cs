using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Evolution.Domain
{
    public abstract class Creature : IAggregateRoot
    {
        protected Creature()
        {
        }

        protected Creature(
            Guid id,
            string name,
            Location location,
            IReadOnlyCollection<Creature> creaturesWithinVisionLimit,
            Guid? parentId,
            IGameCalender calender,
            ILogger<Creature> logger)
        {
            if (id == Guid.Empty) throw new ApplicationException("Id can't be empty");
            if (string.IsNullOrWhiteSpace(name)) throw new ApplicationException("Name can't be empty");

            Id = id;
            Name = name;
            Location = location;
            CreaturesWithinVisionLimit = creaturesWithinVisionLimit ?? new List<Creature>();
            ParentId = parentId;

            CreationTime = calender.Now;

            Calender = calender;
            Logger = logger;
        }

        public GameDays Age => new GameDays(Calender.Now - CreationTime);

        public override bool Equals(object o)
        {
            var other = o as Creature;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == Guid.Empty || other.Id == Guid.Empty)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Creature a, Creature b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Creature a, Creature b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

        public DateTime CreationTime { get; protected set; }
        public DateTime? DeathTime { get; protected set; }
        public Guid Id { get; protected set; }

        public bool IsAlive { get; protected set; }
        public string Name { get; protected set; }
        public Guid? ParentId { get; protected set; }
        public int Weight { get; protected set; }
        public Location Location { get; protected set; }

        protected ILogger<Creature> Logger { get; }
        public abstract bool IsEatableBy(Type otherType);

        protected IReadOnlyCollection<Creature> CreaturesWithinVisionLimit { get; }
        protected IGameCalender Calender { get; }

        public abstract void Act();
        public abstract int EatInto(int desiredAmount);
    }

}
