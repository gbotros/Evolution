using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Services;

namespace Evolution.Apis.Controllers
{
    [Route("apis/[controller]")]
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
