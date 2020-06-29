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

            var neighbor = new LocationBlueprint(targetX, targetY);
            return IsLocationWithinWorldLimits(neighbor) ? neighbor : null;
        }

        public LocationBlueprint GetNorthEastLocation(LocationBlueprint location)
        {
            var targetX = location.X + 1;
            var targetY = location.Y - 1;

            var neighbor = new LocationBlueprint(targetX, targetY);
            return IsLocationWithinWorldLimits(neighbor) ? neighbor : null;
        }

        public LocationBlueprint GetNorthLocation(LocationBlueprint location)
        {
            var targetX = location.X;
            var targetY = location.Y - 1;

            var neighbor = new LocationBlueprint(targetX, targetY);
            return IsLocationWithinWorldLimits(neighbor) ? neighbor : null;
        }

        public LocationBlueprint GetNorthWestLocation(LocationBlueprint location)
        {
            var targetX = location.X - 1;
            var targetY = location.Y - 1;

            var neighbor = new LocationBlueprint(targetX, targetY);
            return IsLocationWithinWorldLimits(neighbor) ? neighbor : null;
        }

        public LocationBlueprint GetSouthEastLocation(LocationBlueprint location)
        {
            var targetX = location.X + 1;
            var targetY = location.Y + 1;

            var neighbor = new LocationBlueprint(targetX, targetY);
            return IsLocationWithinWorldLimits(neighbor) ? neighbor : null;
        }

        public LocationBlueprint GetSouthLocation(LocationBlueprint location)
        {
            var targetX = location.X;
            var targetY = location.Y + 1;

            var neighbor = new LocationBlueprint(targetX, targetY);
            return IsLocationWithinWorldLimits(neighbor) ? neighbor : null;
        }

        public LocationBlueprint GetSouthWestLocation(LocationBlueprint location)
        {
            var targetX = location.X - 1;
            var targetY = location.Y + 1;

            var neighbor = new LocationBlueprint(targetX, targetY);
            return IsLocationWithinWorldLimits(neighbor) ? neighbor : null;
        }

        public LocationBlueprint GetWestLocation(LocationBlueprint location)
        {
            var targetX = location.X - 1;
            var targetY = location.Y;

            var neighbor = new LocationBlueprint(targetX, targetY);
            return IsLocationWithinWorldLimits(neighbor) ? neighbor : null;
        }

        private bool IsLocationWithinWorldLimits(LocationBlueprint location)
        {
            return location.X >= Constants.WorldEdgeStart &&
                   location.Y >= Constants.WorldEdgeStart &&
                   location.X <= Constants.WorldEdgeEnd &&
                   location.Y <= Constants.WorldEdgeEnd;
        }
    }
}