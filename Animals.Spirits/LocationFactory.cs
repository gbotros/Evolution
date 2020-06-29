using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution;
using Evolution.Abstractions;
using Evolution.Entities;

namespace Animals.Spirits
{
    public class LocationFactory : ILocationFactory
    {
        public LocationFactory(ILocationHelper locationHelper, IAnimalService animalService, IPlantService plantService)
        {
            LocationHelper = locationHelper;
            AnimalService = animalService;
            PlantService = plantService;
        }

        public IAnimalService AnimalService { get; }

        public ILocationHelper LocationHelper { get; }
        public IPlantService PlantService { get; }

        public async Task<ILocation> Create(LocationBlueprint blueprint)
        {
            var animals = await AnimalService.GetByLocation(blueprint);
            var plants = await PlantService.GetByLocation(blueprint);
            var neighbors = CreateNeighbors(blueprint);
            var location = new Location(blueprint, animals, plants, neighbors);
            return location;
        }

        public async Task<ILocation> CreateEmpty(LocationBlueprint blueprint)
        {
            return new Location(blueprint);
        }

        private IEnumerable<LocationBlueprint> CreateNeighbors(LocationBlueprint location)
        {
            var neighbors = new List<LocationBlueprint>
            {
                LocationHelper.GetEastLocation(location),
                LocationHelper.GetSouthEastLocation(location),
                LocationHelper.GetSouthLocation(location),
                LocationHelper.GetSouthWestLocation(location),
                LocationHelper.GetWestLocation(location),
                LocationHelper.GetNorthWestLocation(location),
                LocationHelper.GetNorthLocation(location),
                LocationHelper.GetNorthEastLocation(location)
            };

            return neighbors.Where(n => n != null);
        }
    }
}