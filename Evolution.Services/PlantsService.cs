using System;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.PlantAggregate;

namespace Evolution.Services
{
    public class PlantsService : IPlantsService
    {
        private EvolutionContext Context { get; }
        public IPlantsFactory PlantsFactory { get; }

        public PlantsService(EvolutionContext context, IPlantsFactory plantsFactory)
        {
            Context = context;
            PlantsFactory = plantsFactory;
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
            var newPlant = PlantsFactory.CreateNew(parentId);
            await Context.Plants.AddAsync(newPlant);
            await Context.SaveChangesAsync();
        }

        public async Task<Plant> Get(Guid plantId) => await Context.Plants.FindAsync(plantId);
    }
}
