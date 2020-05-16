using Evolution.Abstractions;

namespace Evolution
{
    public class LocationNameHelper : ILocationNameHelper
    {
        private const string IntFormat = "D2";

        public string GetEastLocationName(int x, int y)
        {
            var targetX = x + 1;
            var targetY = y;

            return GetLocationName(targetX, targetY);
        }

        public string GetLocationName(int x, int y)
        {
            return x.ToString(IntFormat) + "," + y.ToString(IntFormat);
        }

        public string GetNorthEastLocationName(int x, int y)
        {
            var targetX = x + 1;
            var targetY = y - 1;

            return GetLocationName(targetX, targetY);
        }

        public string GetNorthLocationName(int x, int y)
        {
            var targetX = x;
            var targetY = y - 1;

            return GetLocationName(targetX, targetY);
        }

        public string GetNorthWestLocationName(int x, int y)
        {
            var targetX = x - 1;
            var targetY = y - 1;

            return GetLocationName(targetX, targetY);
        }

        public string GetSouthEastLocationName(int x, int y)
        {
            var targetX = x + 1;
            var targetY = y + 1;

            return GetLocationName(targetX, targetY);
        }

        public string GetSouthLocationName(int x, int y)
        {
            var targetX = x;
            var targetY = y + 1;

            return GetLocationName(targetX, targetY);
        }

        public string GetSouthWestLocationName(int x, int y)
        {
            var targetX = x - 1;
            var targetY = y + 1;

            return GetLocationName(targetX, targetY);
        }

        public string GetWestLocationName(int x, int y)
        {
            var targetX = x - 1;
            var targetY = y;

            return GetLocationName(targetX, targetY);
        }
    }
}