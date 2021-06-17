using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Web.Services
{
    public class AnimalsDefaultsService : IAnimalsDefaultsService
    {
        private HttpClient Client { get; }
        private const string animalsDefaultsUrl = "animalsDefaults";

        public AnimalsDefaultsService(HttpClient client)
        {
            Client = client;
        }

        public async Task Reset(AnimalDefaultsDto animalDefaultsDto)
        {
           var response = await Client.PutAsJsonAsync(animalsDefaultsUrl, animalDefaultsDto);
        }

    }
}