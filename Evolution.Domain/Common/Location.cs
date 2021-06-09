using System.Collections.Generic;
using System.Linq;

namespace Evolution.Domain.Common
{
    public class Location : ValueObject<Location>
    {
        private static int NameFormattingDigitsCount { get; } = 4;

        public Location(int row, int column) : this()
        {
            Row = row;
            Column = column;
        }

        protected Location()
        {
        }

        public string Name => $"Cell ({Row.ToString($"D{NameFormattingDigitsCount}")}, {Column.ToString($"D{NameFormattingDigitsCount}")})";

        public int Row { get; }

        public int Column { get; }

        public IReadOnlyCollection<Location> GetNeighbours(WorldSize worldSize)
        {

            var neighbours = new List<Location>(){
                    GetUpLocation(),
                    GetDownLocation(),
                    GetRightLocation(),
                    GetLeftLocation(),
                    GetUpLeftLocation(),
                    GetUpRightLocation(),
                    GetDownLeftLocation(),
                    GetDownRightLocation()
                };

            return neighbours.Where(l => l.IsValid(worldSize)).ToList().AsReadOnly();
        }

        public bool IsValid(WorldSize worldSize)
        {
            var worldWidth = worldSize.Width;
            var worldHeight = worldSize.Height;
            const int firstColumn = 0;
            var lastColumn = worldWidth - 1;
            const int firstRow = 0;
            var lastRow = worldHeight - 1;

            var valid = true;
            valid &= Row >= firstColumn;
            valid &= Column >= firstRow;
            valid &= Row <= lastRow;
            valid &= Column <= lastColumn;
            return valid;
        }

        protected override bool EqualsCore(Location other)
        {
            return Row == other.Row && Column == other.Column;
        }

        protected override int GetHashCodeCore()
        {
            return Name.GetHashCode();
        }

        private Location GetUpLocation()
        {
            return new Location(Row - 1, Column);
        }

        private Location GetDownLocation()
        {
            return new Location(Row + 1, Column);

        }

        private Location GetRightLocation()
        {
            return new Location(Row, Column + 1);
        }

        private Location GetLeftLocation()
        {
            return new Location(Row, Column - 1);
        }

        private Location GetUpRightLocation()
        {
            return new Location(Row - 1, Column + 1);
        }

        private Location GetDownRightLocation()
        {
            return new Location(Row + 1, Column + 1);
        }

        private Location GetUpLeftLocation()
        {
            return new Location(Row - 1, Column - 1);
        }

        private Location GetDownLeftLocation()
        {
            return new Location(Row + 1, Column - 1);
        }

    }


}