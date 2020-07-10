using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Abstractions.Dtos;
using Evolution.Entities;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Evolution.Services.Http
{
    public class PlantService : IPlantService
    {
        private const string Uri = "Plants";
        public PlantService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }

        public async Task<IEnumerable<PlantBlueprint>> GetByLocation(LocationBlueprint location)
        {
            if (location == null) return new List<PlantBlueprint>();

            var filter = new Dictionary<string, string>()
            {
                { "LocationX", location.X.ToString(CultureInfo.InvariantCulture) },
                { "LocationY", location.Y.ToString(CultureInfo.InvariantCulture) }
            };

            var url = QueryHelpers.AddQueryString(Uri, filter);

            using var response = await HttpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();
            var creatures = JsonConvert.DeserializeObject<IEnumerable<PlantBlueprint>>(content);
            return creatures;


        }
        public async Task<IEnumerable<PlantBlueprint>> GetAll()
        {
            using var response = await HttpClient.GetAsync(Uri);
            var content = await response.Content.ReadAsStringAsync();
            var creatures = JsonConvert.DeserializeObject<IEnumerable<PlantBlueprint>>(content);
            return creatures;
        }

        public async Task<UpdatePlantResponseDto> Update(UpdatePlantDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            using var response = await HttpClient.PutAsJsonAsync($"{Uri}/{dto.Id}", dto);
            var r = await response.Content.ReadAsStringAsync();
            return await response.Content.ReadAsAsync<UpdatePlantResponseDto>();
        }
    }
}