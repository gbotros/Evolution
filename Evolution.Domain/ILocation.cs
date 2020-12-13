using System;
using System.Collections.Generic;

namespace Evolution.Domain
{
    public interface ILocation
    {
        IEnumerable<ICreature> Community { get; }
        IEnumerable<INeighbourLocation> Neighbours { get; }
        string Name { get; }
        int X { get; }
        int Y { get; }
        bool IsEmpty();
    }
}