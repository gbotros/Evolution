using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Domain.PlantAggregate;
using Evolution.Services;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private IPlantsService Service { get; }

        public PlantController(IPlantsService service)
        {
            Service = service;
        }
        
        [HttpGet]
        public async Task<Plant> Get(Guid id) => await Service.Get(id);

        [HttpPost]
        public async Task Post(Guid? parentId) => await Service.CreateNew(parentId);

        [HttpPut]
        public async Task Put([FromBody] Guid id) => await Service.Act(id);

    }
}
