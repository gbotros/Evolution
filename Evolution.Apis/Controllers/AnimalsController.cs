using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Evolution.Apis.Dtos;
using Evolution.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        public AnimalsController(EvolutionDbContext context, IConfiguration configuration,
            ILogger<AnimalsController> logger)
        {
            Context = context;
            Configuration = configuration;
            Logger = logger;
        }

        public IConfiguration Configuration { get; }
        public ILogger<AnimalsController> Logger { get; }

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
        [HttpPost]
        public async Task<ActionResult<AnimalBlueprint>> PostAnimalBlueprint(AnimalBlueprint animalBlueprint)
        {
            if (animalBlueprint == null) return BadRequest("animal blueprint cannot be null");

            Context.Animals.Add(animalBlueprint);
            animalBlueprint.BirthDate = DateTime.UtcNow;
            animalBlueprint.UpdatedAt = DateTime.UtcNow;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimalBlueprint(Guid id, AnimalBlueprint animalBlueprint)
        {
            if (animalBlueprint == null) return BadRequest("animal blueprint cannot be null");
            if (id != animalBlueprint.Id) return BadRequest("resource id does not match animalBluePrint Id");

            Context.Entry(animalBlueprint).State = EntityState.Modified;
            animalBlueprint.UpdatedAt = DateTime.UtcNow;
            // workaround for updating value object at ef
            animalBlueprint.Location = new LocationBlueprint(animalBlueprint.Location.X, animalBlueprint.Location.Y);

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