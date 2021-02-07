using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.Common;

namespace Evolution.Domain
{
    public class Location : ValueObject<Location>
    {

        private int FirstRow = 0;
        private int FirstColumn = 0;
        private int LastRow;
        private int LastColumn;
        private int WorldWidth;
        private int WorldHeight;
        private int digits;

        public Location(int row, int column, int worldWidth, int worldHeight) : this()
        {
            if (worldWidth < 1 || worldHeight < 1) throw new ApplicationException("World cannot be smaller than one cell");

            WorldWidth = worldWidth;
            WorldHeight = worldHeight;
            LastColumn = worldWidth - 1;
            LastRow = worldHeight - 1;

            if (row < 0 || column < 0) throw new ApplicationException("Cell cannot be created outside the world boundaries");
            if (row > LastRow || column > LastColumn) throw new ApplicationException("Cell cannot be created outside the world boundaries");

            Row = row;
            Column = column;
        }

        protected Location()
        {
        }

        public string Name => $"Cell ({Row.ToString($"D{Digits}")}, {Column.ToString($"D{Digits}")})";

        public int Row { get; }

        public int Column { get; }

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

                return neighbours.Where(l => l != null).ToList().AsReadOnly();
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

        private Location Up
        {
            get
            {
                if (!IsValid(Row - 1, Column)) return null;
                return new Location(Row - 1, Column, WorldWidth, WorldHeight);
            }
        }

        private Location Down
        {
            get
            {
                if (!IsValid(Row + 1, Column)) return null;
                return new Location(Row + 1, Column, WorldWidth, WorldHeight);
            }
        }

        private Location Right
        {
            get
            {

                if (!IsValid(Row, Column + 1)) return null;
                return new Location(Row, Column + 1, WorldWidth, WorldHeight);
            }
        }

        private Location Left
        {
            get
            {
                if (!IsValid(Row, Column - 1)) return null;
                return new Location(Row, Column - 1, WorldWidth, WorldHeight);
            }
        }

        private Location UpRight
        {
            get
            {
                if (!IsValid(Row - 1, Column + 1)) return null;
                return new Location(Row - 1, Column + 1, WorldWidth, WorldHeight);
            }
        }

        private Location DownRight
        {
            get
            {
                if (!IsValid(Row + 1, Column + 1)) return null;
                return new Location(Row + 1, Column + 1, WorldWidth, WorldHeight);
            }
        }

        private Location UpLeft
        {
            get
            {
                if (!IsValid(Row - 1, Column - 1)) return null;
                return new Location(Row - 1, Column - 1, WorldWidth, WorldHeight);
            }
        }

        private Location DownLeft
        {
            get
            {
                if (!IsValid(Row + 1, Column - 1)) return null;
                return new Location(Row + 1, Column - 1, WorldWidth, WorldHeight);
            }
        }

        private bool IsValid(int row, int col)
        {
            var valid = true;
            valid &= row >= FirstRow;
            valid &= col >= FirstColumn;
            valid &= row <= LastRow;
            valid &= col <= LastColumn;
            return valid;
        }

        private int Digits
        {
            get
            {
                if (digits > 0) return digits;

                var max = Math.Max(LastRow, LastColumn);

                digits = max.ToString().Length;
                return digits;
            }
        }

    }


}