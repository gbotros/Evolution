using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Entities;
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

        public async Task<EatIntoOperationResult> EatInto(Guid plantId, int neededAmount)
        {
            return new EatIntoOperationResult {CurrentWeight = 10000, Eaten = neededAmount};
        }

        public async Task<IEnumerable<PlantBlueprint>> GetByLocation(LocationBlueprint location)
        {
            if (location == null) return new List<PlantBlueprint>();

            var parameters = new Dictionary<string, string>
            {
                {"Id", null},
                {"LocationX", location.X.ToString(CultureInfo.InvariantCulture)},
                {"LocationY", location.Y.ToString(CultureInfo.InvariantCulture)}
            };
            using var animalsFilter = new FormUrlEncodedContent(parameters);
            using var response = await HttpClient.GetAsync($"Plants/?{animalsFilter}");

            var content = await response.Content.ReadAsStringAsync();
            var creatures = JsonConvert.DeserializeObject<IEnumerable<PlantBlueprint>>(content);
            return creatures;
        }

        public async Task<bool> Update(PlantBlueprint plant)
        {
            var plantJson = JsonConvert.SerializeObject(plant);
            using var plantContent = new StringContent(plantJson);
            using var response = await HttpClient.PutAsync("Plants", plantContent);

            return response.IsSuccessStatusCode;
        }
    }
}