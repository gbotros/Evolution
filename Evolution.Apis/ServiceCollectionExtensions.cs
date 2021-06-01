using System;
using Evolution.Data;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;
using Evolution.Domain.PlantAggregate;
using Evolution.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.Apis
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton(CreateWorldSize(Configuration));

            services.AddScoped<IPlantsService, PlantsService>();
            services.AddScoped<IPlantsFactory, PlantsFactory>();
            services.AddScoped<IAnimalsService, AnimalsService>();
            services.AddScoped<IAnimalsFactory, AnimalsFactory>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IGameCalender, GameCalender>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
              
            services.AddScoped(provider => new EvolutionContext(connectionString, true));
            
        }

        private static WorldSize CreateWorldSize(IConfiguration Configuration)
        {
            var width = Convert.ToInt32(Configuration["WorldSize:Width"]);
            var height = Convert.ToInt32(Configuration["WorldSize:Height"]);
            var worldSize = new WorldSize(width, height);
            return worldSize;
        }
    }
}