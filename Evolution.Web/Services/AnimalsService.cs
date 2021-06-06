using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Web.Services
{
    public class AnimalsService : IAnimalsService
    {
        private HttpClient Client { get; }
        private const string animalsUrl = "animals";

        public AnimalsService(HttpClient client)
        {
            Client = client;
        }

        public async Task CreateNew(string newAnimalName)
        {
            var response = await Client.PostAsJsonAsync($"{animalsUrl}", newAnimalName);
        }

        public async Task Kill(Guid id)
        {
            await Client.DeleteAsync($"{animalsUrl}/{id}");
        }

        public async Task Act(Guid id)
        {
            await Client.PutAsJsonAsync($"{animalsUrl}/{id}", "");
        }

        public async Task<List<AnimalDto>> GetAll()
        {
            return await Client.GetFromJsonAsync<List<AnimalDto>>(animalsUrl);
        }
        
    }
}
