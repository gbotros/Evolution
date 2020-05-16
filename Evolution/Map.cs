using System.Collections.Generic;
using Evolution.Abstractions;
using Evolution.Blueprints;

namespace Evolution
{
    public class Map
    {
        public Map(int size, ILocationNameHelper nameHelper)
        {
            Size = size;
            NameHelper = nameHelper;
            Initialize();
        }

        public Dictionary<string, ILocation> Locations { get; private set; } = new Dictionary<string, ILocation>();

        public ILocationNameHelper NameHelper { get; }

        public int Size { get; }

        private void Initialize()
        {
            Locations = new Dictionary<string, ILocation>();
            for (var x = 0; x < Size; x++)
            for (var y = 0; y < Size; y++)
            {
                var locationBlueprint = new LocationBlueprint(x, y);
                var spot = new Location(locationBlueprint, NameHelper);
                Locations.Add(spot.Name, spot);
            }

            UpdateNeighbours();
        }

        private ILocation SelectLocation(string name)
        {
            return Locations.ContainsKey(name) ? Locations[name] : null;
        }

        private void UpdateNeighbours()
        {
            foreach (var spotItem in Locations)
            {
                var spot = spotItem.Value;
                var n = NameHelper.GetNorthLocationName(spot.X, spot.Y);
                var ne = NameHelper.GetNorthEastLocationName(spot.X, spot.Y);
                var e = NameHelper.GetEastLocationName(spot.X, spot.Y);
                var se = NameHelper.GetSouthEastLocationName(spot.X, spot.Y);
                var s = NameHelper.GetSouthLocationName(spot.X, spot.Y);
                var sw = NameHelper.GetSouthWestLocationName(spot.X, spot.Y);
                var w = NameHelper.GetWestLocationName(spot.X, spot.Y);
                var nw = NameHelper.GetNorthWestLocationName(spot.X, spot.Y);

                spot.Neighbours =
                    new List<ILocation>
                    {
                        SelectLocation(n),
                        SelectLocation(ne),
                        SelectLocation(e),
                        SelectLocation(se),
                        SelectLocation(s),
                        SelectLocation(sw),
                        SelectLocation(w),
                        SelectLocation(nw)
                    };
            }
        }
    }
}