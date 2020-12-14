using System;
using System.Collections.Generic;

namespace Evolution.Domain
{
    public interface ILocation
    {
        IList<ICreature> Community { get; }
        IEnumerable<ILocation> Neighbours { get; }
        string Name { get; }
        int X { get; }
        int Y { get; }
        bool IsEmpty();
        void Move(ICreature creature, ILocation newLocation);
        void Locate(ICreature creature);
    }
}