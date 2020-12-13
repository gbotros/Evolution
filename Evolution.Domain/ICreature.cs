using System;
using System.Threading.Tasks;

namespace Evolution.Domain
{
    public interface ICreature
    {
        double AgeInDays { get; }
        DateTime BirthDate { get; }
        DateTime? DeathDate { get; }
        Guid Id { get; }
        bool IsAlive { get; }
        string Name { get; }
        int Weight { get; }
        ILocation Location { get; }
        bool IsEatable(ICreature other);
        void Act();
        void EatInto(int neededAmount);
    }
}