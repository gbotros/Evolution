using System.Collections.Generic;
using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface IAnimal : ICreature
    {
        IEnumerable<AnimalBlueprint> Children { get; }
        int ChildrenCount { get; }
        int Speed { get; }
        int Fight(int neededAmount);
        AnimalBlueprint GetBlueprint();
    }
}