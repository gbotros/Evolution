using System.Reflection;
using Evolution.Data;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;
using Evolution.Domain.PlantAggregate;
using Evolution.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.Apis
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<IGameCalender, GameCalender>();
            services.AddSingleton<ILocationService, LocationService>();

            services.AddScoped<IPlantsService, PlantsService>();
            services.AddScoped<IPlantsFactory, PlantsFactory>();
            services.AddScoped<IAnimalsService, AnimalsService>();
            services.AddScoped<IAnimalsFactory, AnimalsFactory>();
            services.AddScoped<IGameSettingsService, GameSettingsService>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton(new EvolutionContextOptions(connectionString, true));
            services.AddScoped<IEvolutionContext, EvolutionContext>();

            services.AddMediatR(typeof(AnimalBornEventHandler).GetTypeInfo().Assembly);
        }
        
    }
}