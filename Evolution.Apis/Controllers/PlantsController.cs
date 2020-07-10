using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Apis.Dtos;
using Evolution.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        public PlantsController(EvolutionDbContext context)
        {
            Context = context;
        }

        private EvolutionDbContext Context { get; }

        // GET: api/Plants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantBlueprint>> GetPlantBlueprint(Guid id)
        {
            var plantBlueprint = await Context.Plants.FindAsync(id);

            if (plantBlueprint == null) return NotFound();

            return plantBlueprint;
        }

        // GET: api/Plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantBlueprint>>> GetPlants([FromQuery] PlantsFilter filter)
        {
            var plants = Context.Plants.AsQueryable();
            if (filter == null) return await plants.ToListAsync();

            if (filter.Id.HasValue && filter.Id != Guid.Empty) plants = plants.Where(a => a.Id == filter.Id);

            if (filter.LocationX.HasValue) plants = plants.Where(a => a.Location.X == filter.LocationX);

            if (filter.LocationY.HasValue) plants = plants.Where(a => a.Location.Y == filter.LocationY);

            return await plants.ToListAsync();
        }

        // POST: api/Plants
        [HttpPost]
        public async Task<ActionResult<PlantBlueprint>> PostPlantBlueprint(PlantBlueprint plantBlueprint)
        {
            if (plantBlueprint == null) return BadRequest("plantBlueprint cannot be null");

            Context.Plants.Add(plantBlueprint);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlantBlueprintExists(plantBlueprint.Id)) return Conflict();
                else throw;
            }

            return CreatedAtAction("GetPlantBlueprint", new {id = plantBlueprint.Id}, plantBlueprint);
        }

        // PUT: api/Plants/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdatePlantResponseDto>> PutPlantBlueprint(Guid id, UpdatePlantDto dto)
        {
            if (dto == null) return BadRequest("dto cannot be null");
            if (id != dto.Id) return BadRequest("resource id does not match dto Id");

            var plant = await Context.Plants.FirstOrDefaultAsync(p => p.Id == id);
            if (plant == null) return NotFound();

            var originalWeight = plant.Weight;
            plant.Weight += dto.Amount;
            if (plant.Weight < 0) plant.Weight = 0;
            plant.IsAlive = dto.IsAlive ?? plant.IsAlive;
            plant.UpdatedAt = DateTime.UtcNow;

            await Context.SaveChangesAsync();

            var amountOfChange = Math.Abs(originalWeight - plant.Weight);
            return new UpdatePlantResponseDto {CurrentWeight = plant.Weight, AmountOfChange = amountOfChange};
        }

        private bool PlantBlueprintExists(Guid id)
        {
            return Context.Plants.Any(e => e.Id == id);
        }
    }
}