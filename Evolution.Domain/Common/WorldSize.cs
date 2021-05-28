namespace Evolution.Domain.Common
{
    public class WorldSize
    {
        public WorldSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }
}
