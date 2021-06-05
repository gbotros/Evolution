using System;

namespace Evolution.Domain.Common
{
    public class WorldSize
    {
        public WorldSize(int width, int height)
        {
            if (width < 1 || height < 1) throw new ApplicationException("World cannot be smaller than one cell");
            
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }
}
