using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;
using Evolution.Dtos;
using Evolution.Services;

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
