using System.Net.Http;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Blueprints;
using Newtonsoft.Json;

namespace Evolution
{
    public class AnimalObserver : IAnimalObserver
    {
        public AnimalObserver(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }

        public async Task<bool> OnEat(AnimalBlueprint animal, ICreature food)
        {
            return await UpdateAnimal(animal);
        }

        public async Task<bool> OnMove(AnimalBlueprint animal)
        {
            return await UpdateAnimal(animal);
        }

        public async Task<bool> OnReproduce(AnimalBlueprint parent, AnimalBlueprint son)
        {
           var parentUpdated = await UpdateAnimal(parent);
           if (parentUpdated) return await AddAnimal(son);
           
           return false;
        }

        private async Task<bool> AddAnimal(AnimalBlueprint animal)
        {
            var animalJson = JsonConvert.SerializeObject(animal);
            var animalContent = new StringContent(animalJson);
            var response = await HttpClient.PostAsync("Animals", animalContent);

            return response.IsSuccessStatusCode;
        }

        private async Task<bool> UpdateAnimal(AnimalBlueprint animal)
        {
            var animalJson = JsonConvert.SerializeObject(animal);
            var animalContent = new StringContent(animalJson);
            var response = await HttpClient.PutAsync("Animals", animalContent);

            return response.IsSuccessStatusCode;
        }
    }
}