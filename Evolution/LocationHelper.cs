using Evolution.Abstractions;
using Evolution.Entities;

namespace Evolution
{
    public class LocationHelper : ILocationHelper
    {
        private const string IntFormat = "D2";

        public LocationBlueprint GetEastLocation(LocationBlueprint location)
        {
            var targetX = location.X + 1;
            var targetY = location.Y;

            return new LocationBlueprint(targetX, targetY);
        }

        public LocationBlueprint GetNorthEastLocation(LocationBlueprint location)
        {
            var targetX = location.X + 1;
            var targetY = location.Y - 1;

            return new LocationBlueprint(targetX, targetY);
        }

        public LocationBlueprint GetNorthLocation(LocationBlueprint location)
        {
            var targetX = location.X;
            var targetY = location.Y - 1;

            return new LocationBlueprint(targetX, targetY);
        }

        public LocationBlueprint GetNorthWestLocation(LocationBlueprint location)
        {
            var targetX = location.X - 1;
            var targetY = location.Y - 1;

            return new LocationBlueprint(targetX, targetY);
        }

        public LocationBlueprint GetSouthEastLocation(LocationBlueprint location)
        {
            var targetX = location.X + 1;
            var targetY = location.Y + 1;

            return new LocationBlueprint(targetX, targetY);
        }

        public LocationBlueprint GetSouthLocation(LocationBlueprint location)
        {
            var targetX = location.X;
            var targetY = location.Y + 1;

            return new LocationBlueprint(targetX, targetY);
        }

        public LocationBlueprint GetSouthWestLocation(LocationBlueprint location)
        {
            var targetX = location.X - 1;
            var targetY = location.Y + 1;

            return new LocationBlueprint(targetX, targetY);
        }

        public LocationBlueprint GetWestLocation(LocationBlueprint location)
        {
            var targetX = location.X - 1;
            var targetY = location.Y;

            return new LocationBlueprint(targetX, targetY);
        }
    }
}