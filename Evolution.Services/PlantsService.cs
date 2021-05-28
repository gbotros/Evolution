using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.Common;
using Evolution.Domain.PlantAggregate;
using Microsoft.Extensions.Logging;

namespace Evolution.Services
{
    public class PlantsService : IPlantsService
    {
        private EvolutionContext Context { get; }
        public IPlantFactory PlantFactory { get; }

        public PlantsService(EvolutionContext context, IPlantFactory plantFactory)
        {
            Context = context;
            PlantFactory = plantFactory;
        }

        public async Task Act(Guid plantId)
        {
            var plant = await Context.Plants.FindAsync(plantId);
            if (plant == null) return;

            plant.Act();

            await Context.SaveChangesAsync();
        }

        public async Task CreateNew(Guid? parentId)
        {
            var newPlant = PlantFactory.CreateNew(parentId);
            await Context.Plants.AddAsync(newPlant);
            await Context.SaveChangesAsync();
        }

        public async Task<Plant> Get(Guid plantId) => await Context.Plants.FindAsync(plantId);
    }
}
