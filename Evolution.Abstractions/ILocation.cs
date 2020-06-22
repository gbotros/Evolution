using System;
using System.Collections.Generic;
using Evolution.Blueprints;
using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface ILocation
    {
        IEnumerable<AnimalBlueprint> Animals { get; }
        LocationBlueprint Blueprint { get; }
        Guid Id { get; set; }
        string Name { get; }
        IEnumerable<ILocation> Neighbours { get; set; }
        IEnumerable<PlantBlueprint> Plants { get; }
        int X { get; }
        int Y { get; }
        bool IsEmpty();
    }
}