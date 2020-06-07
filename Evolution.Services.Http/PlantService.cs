using System;
using System.Net.Http;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Blueprints;
using Newtonsoft.Json;

namespace Evolution.Services.Http
{
    public class PlantService : IPlantService
    {
        public PlantService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }

        public Task<EatIntoOperationResult> EatInto(Guid plantId, int neededAmount)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(PlantBlueprint plant)
        {
            var plantJson = JsonConvert.SerializeObject(plant);
            using var plantContent = new StringContent(plantJson);
            using var response = await HttpClient.PutAsync("Plants", plantContent).ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}