using System;

namespace Evolution.Domain.Common
{
    public class LocationService: ILocationService
    {
        private readonly Random random;

        private WorldSize WorldSize { get; }

        public LocationService(WorldSize worldSize)
        {
            WorldSize = worldSize;
            random = new Random();
        }

        public Location GetRandom()
        {
            var row = random.Next(WorldSize.Height - 1);
            var col = random.Next(WorldSize.Width - 1);
            var location = new Location(row, col, WorldSize.Width, WorldSize.Height);
            return location;
        }
    }
}
