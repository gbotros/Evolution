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
         
        public override bool Equals(object obj)
        {
            var other = obj as Creature;

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

        public DateTime BirthDate { get; protected set; }
        public DateTime? DeathDate { get; protected set; }
        public Guid Id { get; protected set; }
        public bool IsAlive { get; protected set; }
        public string Name { get; protected set; }
        public Guid? ParentId { get; protected set; }
        public int Weight { get; protected set; }
        public ILocation Location { get; protected set; }
        private IGameCalender GameCalender { get; }

        public abstract bool IsEatableBy(ICreature other);

        public abstract void Act();
        public abstract void EatInto(int neededAmount);
    }
}