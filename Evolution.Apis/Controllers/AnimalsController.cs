using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Dtos;
using Evolution.Services;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private IAnimalsService Service { get; }

        public AnimalsController(IAnimalsService service)
        {
            Service = service;
        }

        [HttpGet]
        [Route("{after}")]
        public async Task<IList<AnimalDto>> Get(DateTime after) => await Service.Get(after);

        //[HttpGet]
        //[Route("{id}")]
        //public async Task<AnimalDto> GetAllAlive(Guid id) => await Service.GetAllAlive(id);

        [HttpPost]
        public async Task Post([FromBody] string name) => await Service.CreateNew(name);

        //[HttpPut]
        //public async Task Put() => await Service.Act();

        //[HttpPut]
        //[Route("{id}")]
        //public async Task Put([FromRoute] Guid id) => await Service.Act(id);

        //[HttpDelete]
        //[Route("{id}")]
        //public async Task Delete([FromRoute] Guid id) => await Service.Kill(id);

    }
}
