using System;
using Evolution.Domain.Common;

namespace Evolution.Domain.GameSettingsAggregate
{
    public class WorldSize : ValueObject<WorldSize>
    {
        protected WorldSize()
        {
        }

        public WorldSize(int width, int height)
        {
            if (width < 1 || height < 1) throw new ApplicationException("World cannot be smaller than one cell");

            Width = width;
            Height = height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        protected override bool EqualsCore(WorldSize other)
        {
            return Width == other.Width && Height == other.Height;
        }

        protected override int GetHashCodeCore()
        {
            return $"{Width}-{Height}".GetHashCode();
        }
    }
}
