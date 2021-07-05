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
        private IEvolutionContext Context { get; }
        public IPlantsFactory PlantsFactory { get; }

        public PlantsService(IEvolutionContext context, IPlantsFactory plantsFactory)
        {
            Context = context;
            PlantsFactory = plantsFactory;
        }

        //public async Task GrowAll()
        //{
        //    await Context.GrowAll();
        //}

        public async Task AddFoodAtRandomPlaces(int count)
        {
            for (int i = 0; i < count; i++)
            {
                await CreateNew();
                await Context.SaveChangesAsync();
            }
        }

        //public async Task Act(Guid plantId)
        //{
        //    var plant = await Context.Plants.FindAsync(plantId);
        //    if (plant == null) return;

        //    plant.Act();

        //    await Context.SaveChangesAsync();
        //}

        public async Task CreateNew()
        {
            var settings = Context.GameSettings.First();
            var newPlant = PlantsFactory.CreateNew(settings);
            await Context.Plants.AddAsync(newPlant);
        }

        public async Task DeleteAll()
        {
            Context.Plants.RemoveRange(Context.Plants);
            await Context.SaveChangesAsync();
        }

        public async Task<IList<PlantDto>> GetAllAlive()
        {
            var plants = await Context.Plants.Where(p => p.IsAlive).ToListAsync();
            return plants.Select(MapToDto).ToList();
        }

        //public async Task<PlantDto> Get(Guid plantId)
        //{
        //    var plant = await Context.Plants.FindAsync(plantId);
        //    return MapToDto(plant);
        //}

        private PlantDto MapToDto(Plant plant)
        {
            return new PlantDto()
            {
                Id = plant.Id,
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
