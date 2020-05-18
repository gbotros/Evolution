using System;
using Evolution.Abstractions;
using Evolution.Blueprints;

namespace Evolution.Services.Http
{
    public class LocationService : ILocationService
    {
        public LocationService(IAnimalService animalService, IPlantService plantService)
        {
            AnimalService = animalService;
            PlantService = plantService;
        }

        public IAnimalService AnimalService { get; }
        public IPlantService PlantService { get; }

        public ILocation GetLocation(LocationBlueprint blueprint)
        {
            throw new NotImplementedException();
        }
    }
}