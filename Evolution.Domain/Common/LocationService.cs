using System;
using Evolution.Domain.GameSettingsAggregate;

namespace Evolution.Domain.Common
{
    public class LocationService: ILocationService
    {
        private readonly Random random;
        
        public LocationService()
        {
            random = new Random();
        }

        public Location GetRandom(WorldSize worldSize)
        {
            var row = random.Next(worldSize.Height - 1);
            var col = random.Next(worldSize.Width - 1);
            var location = new Location(row, col);
            return location;
        }
    }
}
