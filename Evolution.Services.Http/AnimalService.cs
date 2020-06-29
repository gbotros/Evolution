using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using Evolution.Abstractions;
using Evolution.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Evolution.Services.Http
{
    public class AnimalService : IAnimalService
    {
        public AnimalService(HttpClient httpClient, ILogger<IAnimalService> logger)
        {
            HttpClient = httpClient;
            Logger = logger;
        }

        public HttpClient HttpClient { get; }
        public ILogger Logger { get; }

        public async Task<bool> Add(AnimalBlueprint animal)
        {
            if (animal == null) throw new ArgumentNullException(nameof(animal));

            using var response = await HttpClient.PostAsJsonAsync("Animals", animal);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<AnimalBlueprint>> GetByLocation(LocationBlueprint location)
        {
            if (location == null) return new List<AnimalBlueprint>();

            var query = HttpUtility.ParseQueryString(string.Empty);
            query["Id"] = null;
            query["LocationX"] = location.X.ToString(CultureInfo.InvariantCulture);
            query["LocationY"] = location.Y.ToString(CultureInfo.InvariantCulture);
            var queryFilter = query.ToString();

            var animals = await HttpClient.GetFromJsonAsync<IEnumerable<AnimalBlueprint>>($"Animals?{queryFilter}");
            return animals;
        }

        public async Task<bool> Update(AnimalBlueprint animal)
        {
            if (animal == null) throw new ArgumentNullException(nameof(animal));

            using var response = await HttpClient.PutAsJsonAsync($"Animals/{animal.Id}", animal);

            return response.IsSuccessStatusCode;
        }
    }
}