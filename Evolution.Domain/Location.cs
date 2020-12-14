using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Domain
{
    public class Location : ILocation
    {
        public Location(int x, int y, IList<ICreature> community, IEnumerable<ILocation> neighbours)
        {
            X = x;
            Y = y; 
            Neighbours = neighbours;
            Community = community;
        }

        public Location()
        {

        }

        private const string IntFormat = "D2";

        public IList<ICreature> Community { get; }
        public IEnumerable<ILocation> Neighbours { get; }

        public Guid Id { get; set; }

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
    }
}