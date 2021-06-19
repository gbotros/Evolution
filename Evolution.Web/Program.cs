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
            builder.Services.AddHttpClient<IAnimalsService, AnimalsService>(client => client.BaseAddress = new Uri("https://localhost:6001/api/"));
            builder.Services.AddHttpClient<IPlantsService, PlantsService>(client => client.BaseAddress = new Uri("https://localhost:6001/api/"));
            builder.Services.AddHttpClient<IGameSettingsService, GameSettingsService>(client => client.BaseAddress = new Uri("https://localhost:6001/api/"));
            
            await builder.Build().RunAsync();
        }
    }
}
