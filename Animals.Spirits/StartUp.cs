using System;
using System.Configuration;
using Evolution;
using Evolution.Abstractions;
using Evolution.Services.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Animals.Spirits.Startup))]
namespace Animals.Spirits
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var httpServiceBaseAddress = Environment.GetEnvironmentVariable("HttpServiceBaseAddress");
            builder.Services.AddHttpClient<IPlantService, PlantService>(client => { client.BaseAddress = new Uri(httpServiceBaseAddress); });
            builder.Services.AddHttpClient<IAnimalService, AnimalService>(client => { client.BaseAddress = new Uri(httpServiceBaseAddress); });
            builder.Services.AddSingleton<ILocationFactory, LocationFactory>();
            builder.Services.AddSingleton<ILocationNameHelper, LocationNameHelper>();
        }
    }
}



