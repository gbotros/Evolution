using System;
using System.Reflection;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;
using Evolution.Domain.GameSettingsAggregate;
using Evolution.Domain.PlantAggregate;
using Evolution.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Evolution.Processor
{
    public class Program
    {
        public static async Task Main()
        {
            var host = Host
                .CreateDefaultBuilder()
                .ConfigureWebJobs(b =>
                {
                    b.AddAzureStorageCoreServices();
                    b.AddTimers();
                })
                .ConfigureAppConfiguration((builderContext, cb) =>
                {
                    var env = builderContext.HostingEnvironment.EnvironmentName;
                    cb.AddJsonFile($"appsettings.json", true, true);
                    cb.AddJsonFile($"appsettings.{env}.json", true, true);
                    cb.AddEnvironmentVariables();
                })
                .ConfigureLogging((context, b) =>
                {
                    b.AddConsole();
                })
                .ConfigureServices((hb, services) =>
                {
                    services.AddScoped<IPlantsService, PlantsService>();
                    services.AddScoped<IPlantsFactory, PlantsFactory>();
                    services.AddScoped<IAnimalsService, AnimalsService>();
                    services.AddScoped<IAnimalsFactory, AnimalsFactory>();
                    services.AddScoped<ILocationService, LocationService>();
                    services.AddScoped<IGameCalender, GameCalender>();
                    var connectionString = hb.Configuration.GetConnectionString("DefaultConnection");
                    services.AddSingleton(new EvolutionContextOptions(connectionString, true));
                    services.AddScoped<IEvolutionContext, EvolutionContext>();
                    services.AddMediatR(typeof(AnimalBornEventHandler).GetTypeInfo().Assembly);
               
                })
                .Build();
            
            using (host)
            {
                await host.RunAsync();
            }
        }
        
    }
}
