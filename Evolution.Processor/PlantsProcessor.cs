using System;
using System.Threading.Tasks;
using Evolution.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Evolution.Processor
{
    public class PlantsProcessor
    {
        private IPlantsService Service { get; }

        public PlantsProcessor(IPlantsService service)
        {
            Service = service;
        }

        [FunctionName("PlantAct")]
        public async Task Run([TimerTrigger("0/10 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            await Service.GrowAll();
        }
    }
}