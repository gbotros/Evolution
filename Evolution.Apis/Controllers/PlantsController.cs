using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Dtos;
using Evolution.Services;

namespace Evolution.Apis.Controllers
{
    [Route("apis/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private IPlantsService Service { get; }

        public PlantsController(IPlantsService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IList<PlantDto>> Get() => await Service.Get();

        [HttpGet]
        [Route("{id}")]
        public async Task<PlantDto> Get(Guid id) => await Service.Get(id);

        [HttpPost]
        public async Task Post() => await Service.CreateNew();

        [HttpPut]
        [Route("{id}")]
        public async Task Put([FromRoute] Guid id) => await Service.Act(id);

    }
}
