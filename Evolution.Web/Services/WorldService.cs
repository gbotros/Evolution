using System.Net.Http;
using System.Threading.Tasks;

namespace Evolution.Web.Services
{
    public class WorldService : IWorldService
    {
        private HttpClient Client { get; }
        private const string worldSizeUrl = "world";

        public WorldService(HttpClient client)
        {
            Client = client;
        }

        public async Task Reset()
        {
            await Client.DeleteAsync(worldSizeUrl);
        }
    }
}