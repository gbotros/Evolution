using System.Collections.Generic;

namespace Evolution.Domain
{
    public interface IAnimal : ICreature
    {
        int ChildrenCount { get; }
        int Speed { get; }
        AnimalBlueprint GetBlueprint();
    }
}