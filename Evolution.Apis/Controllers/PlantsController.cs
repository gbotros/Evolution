using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Blueprints;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly EvolutionDbContext _context;

        public PlantsController(EvolutionDbContext context)
        {
            _context = context;
        }

        // GET: api/Plants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantBlueprint>> GetPlantBlueprint(Guid id)
        {
            var plantBlueprint = await _context.Plants.FindAsync(id);

            if (plantBlueprint == null) return NotFound();

            return plantBlueprint;
        }

        // GET: api/Plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantBlueprint>>> GetPlants()
        {
            return await _context.Plants.ToListAsync();
        }

        // POST: api/Plants
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlantBlueprint>> PostPlantBlueprint(PlantBlueprint plantBlueprint)
        {
            _context.Plants.Add(plantBlueprint);
            try
            {
                await _context.SaveChangesAsync();
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

            _context.Entry(plantBlueprint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}