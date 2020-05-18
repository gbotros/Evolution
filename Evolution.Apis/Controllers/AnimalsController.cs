using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Apis.Dtos;
using Evolution.Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly EvolutionDbContext _context;

        public AnimalsController(EvolutionDbContext context)
        {
            _context = context;
        }
        
        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalBlueprint>> GetAnimalBlueprint(Guid id)
        {
            var animalBlueprint = await _context.Animals.FindAsync(id);

            if (animalBlueprint == null) return NotFound();

            return animalBlueprint;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalBlueprint>>> GetAnimals(AnimalsFilter filter)
        {
            var animals = _context.Animals.AsQueryable();
            if (filter == null) return await animals.ToListAsync();

            if (filter.Id != Guid.Empty) animals = animals.Where(a => a.Id == filter.Id);
            if (filter.LocationX.HasValue) animals = animals.Where(a => a.Location.X == filter.LocationX);
            if (filter.LocationY.HasValue) animals = animals.Where(a => a.Location.Y == filter.LocationY);

            return await animals.ToListAsync();
        }

        // POST: api/Animals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<AnimalBlueprint>> PostAnimalBlueprint(AnimalBlueprint animalBlueprint)
        {
            _context.Animals.Add(animalBlueprint);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AnimalBlueprintExists(animalBlueprint.Id))
                    return Conflict();
                throw;
            }

            return CreatedAtAction("GetAnimalBlueprint", new {id = animalBlueprint.Id}, animalBlueprint);
        }

        // PUT: api/Animals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimalBlueprint(Guid id, AnimalBlueprint animalBlueprint)
        {
            if (id != animalBlueprint.Id) return BadRequest();

            _context.Entry(animalBlueprint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalBlueprintExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        private bool AnimalBlueprintExists(Guid id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }
    }
}