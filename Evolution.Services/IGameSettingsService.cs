using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Services
{
    public interface IGameSettingsService
    {
        Task UpdateOrInsert(GameSettingsDto dto);
        Task<GameSettingsDto> Get();
    }
}