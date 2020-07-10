using Evolution.Abstractions;
using Evolution.Entities;

namespace Evolution.Factories
{
    public class PlantFactory : IPlantFactory
    {
        public PlantFactory(IPlantService plantService, IGameCalender gameCalender)
        {
            PlantService = plantService;
            GameCalender = gameCalender;
        }

        private IGameCalender GameCalender { get; }
        private IPlantService PlantService { get; }

        public IPlant Create(PlantBlueprint plantBlueprint)
        {
            return new Plant(plantBlueprint, PlantService, GameCalender);
        }
    }
}