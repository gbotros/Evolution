using System.Net.Http;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Blueprints;
using Newtonsoft.Json;

namespace Evolution.Services.Http
{
    public class AnimalService : IAnimalService
    {
        public AnimalService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }

        public async Task<bool> Add(AnimalBlueprint animal)
        {
            var animalJson = JsonConvert.SerializeObject(animal);
            var animalContent = new StringContent(animalJson);
            var response = await HttpClient.PostAsync("Animals", animalContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(AnimalBlueprint animal)
        {
            var animalJson = JsonConvert.SerializeObject(animal);
            var animalContent = new StringContent(animalJson);
            var response = await HttpClient.PutAsync("Animals", animalContent);

            return response.IsSuccessStatusCode;
        }
    }
}