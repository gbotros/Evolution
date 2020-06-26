using System;
using System.Threading.Tasks;
using Evolution;
using Evolution.Abstractions;
using Evolution.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.RetryPolicies;
using Newtonsoft.Json;

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

        private static TimeSpan TimeToLive { get; } = TimeSpan.FromDays(7);

        [FunctionName("AnimalsFunction")]
        public async Task Run(
            [QueueTrigger("animals", Connection = "EvolutionStorageConnection")]
            AnimalBlueprint animalBlueprint,
            [Queue("animals")] [StorageAccount("EvolutionStorageConnection")]
            CloudQueue animalsOutputQueue,
            ILogger log)
        {
            try
            {
                var animal = new Animal(animalBlueprint, AnimalService, LocationFactory, log);
                await animal.Act();
                if (animal.IsAlive) await PublishAnimalMessage(animalsOutputQueue, animal);

                log.LogInformation($"Animal acted successfully: {JsonConvert.SerializeObject(animalBlueprint)}");
            }
            catch (Exception ex)
            {
                log.LogError(ex,
                    $"Error at AnimalsFunction processing: {JsonConvert.SerializeObject(animalBlueprint)}");
                throw;
            }
        }

        private static async Task PublishAnimalMessage(CloudQueue animalsOutputQueue, Animal animal)
        {
            var newBlueprint = animal.GetBlueprint();
            var newMessage = new CloudQueueMessage(JsonConvert.SerializeObject(newBlueprint));
            var waitSeconds = Constants.GameHourToRealSecondRatio / animal.Speed;
            var waitTime = TimeSpan.FromSeconds(waitSeconds);
            var requestOptions = new QueueRequestOptions {RetryPolicy = new ExponentialRetry()};
            var operationContext = new OperationContext();
            await animalsOutputQueue.AddMessageAsync(newMessage, TimeToLive, waitTime, requestOptions,
                operationContext);
        }
    }
}