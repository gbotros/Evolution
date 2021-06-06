using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Web.Services
{
    public class WorldSizeService : IWorldSizeService 
    {
        private HttpClient Client { get; }
        private const string worldSizeUrl = "worldSize";

        public WorldSizeService(HttpClient client)
        {
            Client = client;
        }
        
        public async Task<WorldSizeDto> GetWorldSize()
        {
            return await Client.GetFromJsonAsync<WorldSizeDto>(worldSizeUrl);
        }
    }
}