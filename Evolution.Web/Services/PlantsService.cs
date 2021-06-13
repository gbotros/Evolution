using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Web.Services
{
    public class PlantsService : IPlantsService
    {
        private HttpClient Client { get; }
        private const string plantsUrl = "plants";

        public PlantsService(HttpClient client)
        {
            Client = client;
        }

        public async Task CreateNew()
        {
            var response = await Client.PostAsJsonAsync($"{plantsUrl}", "");
        }

        public async Task Kill(Guid id)
        {
            await Client.DeleteAsync($"{plantsUrl}/{id}");
        }

        public async Task Act(Guid id)
        {
            await Client.PutAsJsonAsync($"{plantsUrl}/{id}", "");
        }

        public async Task<List<PlantDto>> GetAll()
        {
            return await Client.GetFromJsonAsync<List<PlantDto>>(plantsUrl);
        }

    }

    public interface IPlantsService
    {
        Task CreateNew();
        Task Kill(Guid id);
        Task Act(Guid id);
        Task<List<PlantDto>> GetAll();
        
    }
}
