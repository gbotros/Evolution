using System;
using System.Threading.Tasks;

namespace Evolution.Abstractions
{
    public interface ICreature
    {
        int BirthDay { get; }
        int? DeathDay { get; }
        Guid Id { get; }
        bool IsAlive { get; }
        ILocation Location { get; }
        string Name { get; }
        int Weight { get; }
        Task Act();
        Task<int> EatInto(int neededAmount);
    }
}