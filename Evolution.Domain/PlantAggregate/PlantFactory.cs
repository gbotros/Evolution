using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Evolution.Domain.Common;
using Microsoft.Extensions.Logging;

namespace Evolution.Domain.PlantAggregate
{
    public class PlantFactory : IPlantFactory
    {
        private WorldSize WorldSize { get; }
        private IGameCalender GameCalender { get; }
        private ILogger<IPlantFactory> Logger { get; }
        private ILogger<Plant> PlantLogger { get; }

        public PlantFactory(WorldSize worldSize,
            IGameCalender gameCalender,
            ILogger<IPlantFactory> logger,
            ILogger<Plant> plantLogger
            )
        {
            WorldSize = worldSize;
            GameCalender = gameCalender;
            Logger = logger;
            PlantLogger = plantLogger;
        }

        public Plant CreateNew(Guid? parentId)
        {
            var id = Guid.NewGuid();
            var random = new Random();

            var row = random.Next(WorldSize.Height - 1);
            var col = random.Next(WorldSize.Width - 1);
            var location = new Location(row, col, WorldSize.Width, WorldSize.Height);
            var plantName = $"plant{DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)}";
            var plant = new Plant(
                id,
                plantName,
                location,
                parentId,
                GameCalender,
                PlantLogger
            );

            return plant;
        }
    }
}
