using System;
using Evolution.Domain.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.Apis
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton(CreateWorldSize(Configuration));
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