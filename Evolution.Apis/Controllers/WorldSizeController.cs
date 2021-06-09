using Microsoft.AspNetCore.Mvc;
using Evolution.Domain.Common;
using Evolution.Dtos;

namespace Evolution.Apis.Controllers
{
    [Route("apis/[controller]")]
    [ApiController]
    public class WorldSizeController : ControllerBase
    {
        private WorldSize WorldSize { get; }

        public WorldSizeController(WorldSize worldSize)
        {
            WorldSize = worldSize;
        }

        [HttpGet]
        public WorldSizeDto Get() => new WorldSizeDto() { Height = WorldSize.Height, Width = WorldSize.Width };


    }
}
