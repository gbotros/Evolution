using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Web.Services
{
    public interface IGameSettingsService
    {
        Task Reset(GameSettingsDto animalDefaultsDto);
        Task<GameSettingsDto> Get( );
    }
}