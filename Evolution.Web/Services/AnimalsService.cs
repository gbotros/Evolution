using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
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

        public async Task<List<AnimalDto>> GetAll(DateTime after)
        {
            var afterFormatted = after.ToString("o", System.Globalization.CultureInfo.InvariantCulture);
            var encoded = HttpUtility.UrlEncode($"{afterFormatted}");
            return await Client.GetFromJsonAsync<List<AnimalDto>>($"{animalsUrl}/{encoded}");
        }

    }
}
