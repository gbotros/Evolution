using Evolution.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace Evolution.Test.ConsoleClient
{
    class Program
    {
        private const string url = "https://localhost:6001";

        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IAnimalsClient, AnimalsClient>();
            services.AddHttpClient<IAnimalsClient, AnimalsClient>(client =>
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });
            var provider = services.BuildServiceProvider();

            var animalsClient = provider.GetService<IAnimalsClient>() ?? throw new NullReferenceException("IAnimalsClient");
            var animals = await animalsClient.GetAll();

            while (true)
            {
                foreach (var animal in animals)
                {
                    await animalsClient.Act(animal.Id);
                }
            }

        }
    }

    public class AnimalsClient : IAnimalsClient
    {
        private HttpClient Client { get; }

        public AnimalsClient(HttpClient client)
        {
            Client = client;
        }

        public async Task<List<AnimalDto>> GetAll()
        {
            return await Client.GetFromJsonAsync<List<AnimalDto>>("apis/animals");
        }

        public async Task Act(Guid id)
        {
          var response =  await Client.PutAsJsonAsync($"apis/animals/{id}", "");
        }
    }

    public interface IAnimalsClient
    {
        Task<List<AnimalDto>> GetAll();
        Task Act(Guid id);
    }
}
