using Evolution.Abstractions;
using Evolution.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Animals.Spirits
{
    public class AnimalsFunction
    {
        public IPlantService PlantService { get; }
        public IAnimalService AnimalService { get; }
        public ILocationService LocationService { get; }
        public ILocationNameHelper LocationNameHelper { get; }

        public AnimalsFunction(
            IPlantService plantService,
            IAnimalService animalService,
            ILocationService locationService,
            ILocationNameHelper locationNameHelper)
        {
            PlantService = plantService;
            AnimalService = animalService;
            LocationService = locationService;
            LocationNameHelper = locationNameHelper;
        }

        [FunctionName("AnimalsFunction")]
        public void Run(
            [QueueTrigger("animals", Connection = "EvolutionStorageConnection")] AnimalBlueprint animalBlueprint,
            [Queue("animals"), StorageAccount("EvolutionStorageConnection")] ICollector<AnimalBlueprint> animalsOutputQueue,
            ILogger log)
        {
            var s = animalBlueprint;
            animalBlueprint.Speed++;
            animalsOutputQueue.Add(animalBlueprint);
            log.LogInformation($"C# Queue trigger function processed: {animalBlueprint}");
        }
    }
}
