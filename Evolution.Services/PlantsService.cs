using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.PlantAggregate;
using Evolution.Dtos;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IList<PlantDto>> Get()
        {
            var plants = await Context.Plants.ToListAsync();
            return plants.Select(MapToDto).ToList();
        }

        public async Task<PlantDto> Get(Guid plantId)
        {
            var plant = await Context.Plants.FindAsync(plantId);
            return MapToDto(plant);
        }

        private PlantDto MapToDto(Plant plant)
        {
            return new PlantDto()
            {
                CreationTime = plant.CreationTime,
                DeathTime = plant.DeathTime,
                IsAlive = plant.IsAlive,
                Name = plant.Name,
                ParentId = plant.ParentId,
                Weight = plant.Weight,
                Location = new LocationDto()
                {
                    Column = plant.Location.Column,
                    Row = plant.Location.Row
                }
            };
        }
    }
}
