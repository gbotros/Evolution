using System;
using System.Threading.Tasks;
using Evolution;
using Evolution.Abstractions;
using Evolution.Factories;
using Evolution.Services.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Animals.Spirits
{
    public class PlantsFunction
    {
        private IPlantService PlantService { get; }
        private IPlantFactory PlantFactory { get; }

        public PlantsFunction(
            IPlantService plantService,
            IPlantFactory plantFactory,
            IGameCalender gameCalender)
        {
            PlantService = plantService;
            PlantFactory = plantFactory;
        }

        [FunctionName("PlantsFunction")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger logger)
        {
            try
            {
                var plantBlueprints = await PlantService.GetAll();
                foreach (var plantBlueprint in plantBlueprints)
                {
                    var plant = PlantFactory.Create(plantBlueprint);
                    await plant.Act();
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error at PlantsFunction");
                throw;
            }
        }
    }
}
