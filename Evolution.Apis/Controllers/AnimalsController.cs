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
    public class AnimalsController : ControllerBase
    {
        private IAnimalsService Service { get; }

        public AnimalsController(IAnimalsService service)
        {
            Service = service;
        }

        [HttpGet]
        public async Task<IList<AnimalDto>> Get() => await Service.Get();

        [HttpGet]
        [Route("{id}")]
        public async Task<AnimalDto> Get(Guid id) => await Service.Get(id);

        [HttpPost]
        public async Task Post([FromBody] string name) => await Service.CreateNew(name);

        [HttpPut]
        [Route("{id}")]
        public async Task Put([FromRoute] Guid id) => await Service.Act(id);
        
        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute] Guid id) => await Service.Kill(id);

    }
}
