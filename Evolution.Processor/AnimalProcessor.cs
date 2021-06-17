using System;
using System.Threading.Tasks;
using Evolution.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Evolution.Processor
{
    public class AnimalProcessor
    {
        private IAnimalsService Service { get; }

        public AnimalProcessor(IAnimalsService service)
        {
            Service = service;
        }

        [FunctionName("AnimalAct")]
        public async Task Run([TimerTrigger("* * * * * *")] TimerInfo myTimer, ILogger log)
        {
            if (myTimer.IsPastDue) log.LogInformation("Timer is running late!");
            log.LogInformation($"Animal Act Timer triggered at: {DateTime.Now}");

            await Service.Act();
        }
    }
}