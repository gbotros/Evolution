using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Apis.Dtos;
using Evolution.Blueprints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private  EvolutionDbContext Context { get; }

        public PlantsController(EvolutionDbContext context)
        {
            Context = context;
        }

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
        public async Task<ActionResult<IEnumerable<PlantBlueprint>>> GetPlants([FromQuery]PlantsFilter filter)
        {
            var plants = Context.Plants.AsQueryable();
            if (filter == null) return await plants.ToListAsync();

            if (filter.Id.HasValue && filter.Id != Guid.Empty) plants = plants.Where(a => a.Id == filter.Id);
            if (filter.LocationX.HasValue) plants = plants.Where(a => a.Location.X == filter.LocationX);
            if (filter.LocationY.HasValue) plants = plants.Where(a => a.Location.Y == filter.LocationY);

            return await plants.ToListAsync();
        }

        // POST: api/Plants
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlantBlueprint>> PostPlantBlueprint(PlantBlueprint plantBlueprint)
        {
            Context.Plants.Add(plantBlueprint);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlantBlueprintExists(plantBlueprint.Id))
                    return Conflict();
                throw;
            }

            return CreatedAtAction("GetPlantBlueprint", new {id = plantBlueprint.Id}, plantBlueprint);
        }

        // PUT: api/Plants/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantBlueprint(Guid id, PlantBlueprint plantBlueprint)
        {
            if (id != plantBlueprint.Id) return BadRequest();

            Context.Entry(plantBlueprint).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantBlueprintExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        private bool PlantBlueprintExists(Guid id)
        {
            return Context.Plants.Any(e => e.Id == id);
        }
    }
}