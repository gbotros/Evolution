using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolution.Domain
{
    public class Location : ILocation, IValueObject
    {
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Location()
        {

        }

        private const string IntFormat = "D2";

        public IEnumerable<ICreature> Community { get; }
        public IEnumerable<INeighbourLocation> Neighbours { get; }

        public Guid Id { get; set; }

        public string Name => X.ToString(IntFormat) + "," + Y.ToString(IntFormat);

        public int X { get; }

        public int Y { get; }

        public bool IsEmpty()
        {
            return !Community.Any();
        }
    }
}