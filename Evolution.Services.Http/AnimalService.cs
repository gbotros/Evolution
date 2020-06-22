using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Evolution.Abstractions;
using Evolution.Entities;
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
            using var animalContent = new StringContent(animalJson);
            using var response = await HttpClient.PostAsync("Animals", animalContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(AnimalBlueprint animal)
        {
            var animalJson = JsonConvert.SerializeObject(animal);
            using var animalContent = new StringContent(animalJson);
            using var response = await HttpClient.PutAsync("Animals", animalContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<AnimalBlueprint>> GetByLocation(LocationBlueprint location)
        {
            if (location == null)
            {
                return new List<AnimalBlueprint>();
            }

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["Id"] = null;
            query["LocationX"] = location.X.ToString(CultureInfo.InvariantCulture);
            query["LocationY"] = location.Y.ToString(CultureInfo.InvariantCulture);
            var queryFilter = query.ToString();
              
            using var response = await HttpClient.GetAsync($"Animals?{queryFilter}");

            var content = await response.Content.ReadAsStringAsync();
            var creatures = JsonConvert.DeserializeObject<IEnumerable<AnimalBlueprint>>(content);
            return creatures;
        }
    }
     
}