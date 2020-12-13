using System;
using System.Threading.Tasks;

namespace Evolution.Domain
{
    public abstract class Creature : ICreature
    {
        protected Creature(IGameCalender gameCalender)
        {
            GameCalender = gameCalender;
        }

        public double AgeInDays => GameCalender.CalculateDifferenceInGameDays(BirthDate, DateTime.UtcNow);

        public DateTime BirthDate { get; protected set; }
        public DateTime? DeathDate { get; protected set; }
        public Guid Id { get; protected set; }
        public bool IsAlive { get; protected set; }
        public string Name { get; protected set; }
        public Guid? ParentId { get; protected set; }
        public int Weight { get; protected set; }
        public ILocation Location { get; protected set; }
        private IGameCalender GameCalender { get; }

        public abstract bool IsEatable(ICreature other);

        public abstract void Act();
        public abstract void EatInto(int neededAmount);
    }
}