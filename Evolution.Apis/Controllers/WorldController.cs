using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Evolution.Services;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorldController : ControllerBase
    {
        private IAnimalsService AnimalsService { get; }
        private IPlantsService PlantsService { get; }

        public WorldController(IAnimalsService animalsService, IPlantsService plantsService)
        {
            AnimalsService = animalsService;
            PlantsService = plantsService;
        }

        [HttpDelete]
        public async Task Reset()
        {
            await AnimalsService.DeleteAll();
            await PlantsService.DeleteAll();
        }
    }
}
