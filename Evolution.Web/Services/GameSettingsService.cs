using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Web.Services
{
    public class GameSettingsService : IGameSettingsService
    {
        private HttpClient Client { get; }
        private const string gameSettingsUrl = "GameSettings";

        public GameSettingsService(HttpClient client)
        {
            Client = client;
        }

        public async Task Reset(GameSettingsDto dto)
        {
           var response = await Client.PutAsJsonAsync(gameSettingsUrl, dto);
        }

        public async Task<GameSettingsDto> Get()
        {
            return await Client.GetFromJsonAsync<GameSettingsDto>(gameSettingsUrl);
        }
    }
}