using System;
using System.Threading.Tasks;
using Evolution.Web.Models;
using Evolution.Web.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddSingleton(new WorldStore());
            builder.Services.AddHttpClient<IAnimalsService, AnimalsService>(client => client.BaseAddress = new Uri("https://localhost:6001/apis/"));
            builder.Services.AddHttpClient<IPlantsService, PlantsService>(client => client.BaseAddress = new Uri("https://localhost:6001/apis/"));
            builder.Services.AddHttpClient<IWorldSizeService, WorldSizeService>(client => client.BaseAddress = new Uri("https://localhost:6001/apis/"));
            builder.Services.AddHttpClient<IWorldService, WorldService>(client => client.BaseAddress = new Uri("https://localhost:6001/apis/"));


            await builder.Build().RunAsync();
        }
    }
}
