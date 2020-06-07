using System;
using Evolution.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Animals.Spirits
{
    public static class AnimalsFunction
    {
        [FunctionName("AnimalsFunction")]
        public static void Run(
            [QueueTrigger("animals", Connection = "EvolutionStorageConnection")] AnimalBlueprint animalBluePrint,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {animalBluePrint}");
        }
    }
}
