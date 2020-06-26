using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Evolution.Apis.Dtos;
using Evolution.Apis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        public AnimalsController(EvolutionDbContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private EvolutionDbContext Context { get; }

        // GET: api/Animals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalBlueprint>> GetAnimalBlueprint(Guid id)
        {
            var animalBlueprint = await Context.Animals.FindAsync(id);

            if (animalBlueprint == null) return NotFound();

            return animalBlueprint;
        }

        // GET: api/Animals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalBlueprint>>> GetAnimals([FromQuery] AnimalsFilter filter)
        {
            var animals = Context.Animals.AsQueryable();
            if (filter == null) return await animals.ToListAsync();

            if (filter.Id.HasValue && filter.Id != Guid.Empty) animals = animals.Where(a => a.Id == filter.Id);

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
            Context.Animals.Add(animalBlueprint);
            try
            {
                await Context.SaveChangesAsync();
                await PublishToQueue(animalBlueprint);
            }
            catch (DbUpdateException)
            {
                if (AnimalBlueprintExists(animalBlueprint.Id)) return Conflict();

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

            Context.Entry(animalBlueprint).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalBlueprintExists(id)) return NotFound();

                throw;
            }

            return NoContent();
        }

        private bool AnimalBlueprintExists(Guid id)
        {
            return Context.Animals.Any(e => e.Id == id);
        }

        private async Task<bool> PublishToQueue(AnimalBlueprint animal)
        {
            var connectionString = Configuration.GetConnectionString("EvolutionStorageConnection");
            var queueClient = new QueueClient(connectionString, "animals");

            if (!queueClient.Exists()) return false;

            var animalMessage = JsonConvert.SerializeObject(animal);
            var animalMessageBytes = Encoding.UTF8.GetBytes(animalMessage);
            var animalMessageEncoded = Convert.ToBase64String(animalMessageBytes);
            await queueClient.SendMessageAsync(animalMessageEncoded);

            return true;
        }
    }
}