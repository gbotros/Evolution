using System.Threading.Tasks;
using Evolution;
using Evolution.Abstractions;
using Evolution.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Animals.Spirits
{
    public class AnimalsFunction
    {
        public AnimalsFunction(
            IPlantService plantService,
            IAnimalService animalService,
            ILocationFactory locationFactory,
            ILocationHelper locationNameHelper)
        {
            PlantService = plantService;
            AnimalService = animalService;
            LocationFactory = locationFactory;
            LocationNameHelper = locationNameHelper;
        }

        public IAnimalService AnimalService { get; }
        public ILocationFactory LocationFactory { get; }
        public ILocationHelper LocationNameHelper { get; }
        public IPlantService PlantService { get; }

        [FunctionName("AnimalsFunction")]
        public async Task Run(
            [QueueTrigger("animals", Connection = "EvolutionStorageConnection")]
            AnimalBlueprint animalBlueprint,
            [Queue("animals")] [StorageAccount("EvolutionStorageConnection")]
            ICollector<AnimalBlueprint> animalsOutputQueue,
            ILogger log)
        {
            var animal = new Animal(animalBlueprint, AnimalService, LocationFactory, log);
            await animal.Act();
            if (animal.IsAlive) animalsOutputQueue.Add(animal.GetBlueprint());

            log.LogInformation($"Animal acted successfully: {animalBlueprint}");
        }
    }
}