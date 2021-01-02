using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Common;

namespace Evolution.Domain
{
    public class Location : ValueObject<Location>
    {

        private const int FirstRow = 0;
        private const int FirstColumn = 0;
        private int LastRow;
        private int LastColumn;

        public Location(int row, int column)
        {
            Row = row;
            Column = column;

            LastColumn = GameSettings.World.Width - 1;
            LastRow = GameSettings.World.Height - 1;
        }

        public Location()
        {

        }

        private const string IntFormat = "D2";

        public string Name => Row.ToString(IntFormat) + "," + Column.ToString(IntFormat);

        public int Row { get; }

        public int Column { get; }

        private Location Up => new Location(Row - 1, Column);
        private Location Down => new Location(Row + 1, Column);
        private Location Right => new Location(Row, Column + 1);
        private Location Left => new Location(Row, Column - 1);


        private Location UpRight => new Location(Row - 1, Column + 1);
        private Location DownRight => new Location(Row + 1, Column + 1);
        private Location UpLeft => new Location(Row - 1, Column - 1);
        private Location DownLeft => new Location(Row + 1, Column - 1);

        private bool IsValid()
        {
            var valid = true;
            valid &= Row >= FirstRow;
            valid &= Column >= FirstColumn;
            valid &= Row <= LastRow;
            valid &= Column <= LastColumn;
            return valid;
        }

        public IReadOnlyCollection<Location> Neighbours
        {
            get
            {
                var neighbours = new List<Location>(){
                    Up,
                    Down,
                    Right,
                    Left,
                    UpLeft,
                    UpRight,
                    DownLeft,
                    DownRight
                };

                return neighbours.AsReadOnly();

            }
        }

        protected override bool EqualsCore(Location other)
        {
            return Row == other.Row && Column == other.Column;
        }

        protected override int GetHashCodeCore()
        {
            return Name.GetHashCode();
        }
    }


}