using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Evolution.Domain
{
    public abstract class Creature : Entity, IAggregateRoot
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
            ILogger<Creature> logger) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ApplicationException("Name can't be empty");

            Id = id;
            Name = name;
            Location = location;
            CreaturesWithinVisionLimit = creaturesWithinVisionLimit ?? new List<Creature>();
            ParentId = parentId;
            IsAlive = true;

            CreationTime = calender.Now;

            Calender = calender;
            Logger = logger;
        }

        public GameDays Age => new GameDays(Calender.Now - CreationTime);

        public DateTime CreationTime { get; protected set; }
        public DateTime? DeathTime { get; protected set; }

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
