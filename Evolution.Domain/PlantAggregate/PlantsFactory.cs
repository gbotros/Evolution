using System;
using System.Globalization;
using Evolution.Domain.Common;
using Evolution.Services;

namespace Evolution.Domain.PlantAggregate
{
    public class PlantsFactory : IPlantsFactory
    {
        public ILocationService LocationService { get; }
        private IGameCalender GameCalender { get; }

        public PlantsFactory(
            ILocationService locationService,
            IGameCalender gameCalender)
        {
            LocationService = locationService;
            GameCalender = gameCalender;
        }

        public Plant CreateNew(Guid? parentId)
        {
            var id = Guid.NewGuid();
            var location = LocationService.GetRandom();
            var plantName = $"plant{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}";
            var plant = new Plant(id, plantName, location, parentId, GameCalender.Now);

            return plant;
        }
    }
}
