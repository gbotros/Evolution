using System;
using System.Collections.Generic;
using Evolution.Blueprints;

namespace Evolution.Abstractions
{
    public interface ILocation
    {
        IEnumerable<IAnimal> Animals { get; }
        LocationBlueprint Blueprint { get; }
        IEnumerable<ICreature> Creatures { get; }
        Guid Id { get; set; }
        string Name { get; }
        IEnumerable<ILocation> Neighbours { get; set; }
        IEnumerable<IPlant> Plants { get; }
        int X { get; }
        int Y { get; }
        bool IsEmpty();
    }
}