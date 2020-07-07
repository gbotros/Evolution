using System;
using System.Threading.Tasks;

namespace Evolution.Abstractions
{
    public interface ICreature
    {
        double AgeInDays { get; }
        DateTime BirthDate { get; }
        DateTime? DeathDate { get; }
        Guid Id { get; }
        bool IsAlive { get; }
        ILocation Location { get; }
        string Name { get; }
        int Weight { get; }
        Task Act();
        Task<int> EatInto(int neededAmount);
    }
}