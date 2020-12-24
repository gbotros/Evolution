using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Common;

namespace Evolution.Domain
{
    public class Location : ILocation
    {
        public Location(int x, int y, IList<ICreature> community, IList<ILocation> neighbours = null)
        {
            X = x;
            Y = y; 
            Neighbours = neighbours ?? new List<ILocation>();
            Community = community ?? new List<ICreature>();
        }

        public Location()
        {

        }

        private const string IntFormat = "D2";

        public IList<ICreature> Community { get; }
        public IList<ILocation> Neighbours { get; }

        public string Name => X.ToString(IntFormat) + "," + Y.ToString(IntFormat);

        public int X { get; }

        public int Y { get; }

        public bool IsEmpty()
        {
            return !Community.Any();
        }

        public void Move(ICreature creature, ILocation newLocation)
        {
            this.Community.Remove(creature);
            newLocation.Locate(creature);
        }

        public void Locate(ICreature creature)
        {
          this.Community.Add(creature);
        }

        public void AddNeighbor(ILocation neighbor)
        {
            Neighbours.Add(neighbor);
        }
    }
}