using System;
using System.IO;
using Animals.Spirits;
using Evolution;
using Evolution.Abstractions;
using Evolution.Services.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Animals.Spirits
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configurationRoot = ReadConfiguration(builder);

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configurationRoot).CreateLogger();

            //Serilog.Debugging.SelfLog.Enable(msg =>
            //{
            //    Debug.Print(msg);
            //    Debugger.Break();
            //});

            builder.Services.AddLogging(loggingBuilder => { loggingBuilder.AddSerilog(Log.Logger); });

            var httpServiceBaseAddress = Environment.GetEnvironmentVariable("HttpServiceBaseAddress");
            builder.Services.AddHttpClient<IPlantService, PlantService>(client =>
            {
                client.BaseAddress = new Uri(httpServiceBaseAddress);
            });
            builder.Services.AddHttpClient<IAnimalService, AnimalService>(client =>
            {
                client.BaseAddress = new Uri(httpServiceBaseAddress);
            });
            builder.Services.AddSingleton<ILocationFactory, LocationFactory>();
            builder.Services.AddSingleton<ILocationHelper, LocationHelper>();
        }

        public static IConfiguration ReadConfiguration(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", true, true) // load config for local machine
                .AddEnvironmentVariables() // load config for deployed function
                .Build();

            return configuration;
        }
    }
}