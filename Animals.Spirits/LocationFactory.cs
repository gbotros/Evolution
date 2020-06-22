using System;
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
        public LocationFactory(ILocationNameHelper nameHelper, IAnimalService animalService, IPlantService plantService)
        {
            NameHelper = nameHelper;
            AnimalService = animalService;
            PlantService = plantService;
        }

        public ILocationNameHelper NameHelper { get; }
        public IAnimalService AnimalService { get; }
        public IPlantService PlantService { get; }

        public async Task<ILocation> Create(LocationBlueprint blueprint)
        {
            var animals = await AnimalService.GetByLocation(blueprint);
            var plants = await PlantService.GetByLocation(blueprint); 
            var location = new Location(blueprint, NameHelper, animals, plants);
            return location;
        }

    }


}
