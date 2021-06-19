using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Evolution.Dtos;
using Evolution.Services;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSettingsController : ControllerBase
    {
        private IGameSettingsService GameSettingsService { get; }
        private IAnimalsService AnimalsService { get; }
        private IPlantsService PlantsService { get; }

        public GameSettingsController(IGameSettingsService gameSettingsService, IAnimalsService animalsService, IPlantsService plantsService)
        {
            GameSettingsService = gameSettingsService;
            AnimalsService = animalsService;
            PlantsService = plantsService;
        }

        [HttpPut]
        public async Task Reset(GameSettingsDto dto)
        {
            await GameSettingsService.UpdateOrInsert(dto);
            await AnimalsService.DeleteAll();
            await PlantsService.DeleteAll();
        }

        [HttpGet]
        public async Task<GameSettingsDto> Get()
        {
            return await GameSettingsService.Get();
        }
    }
}
