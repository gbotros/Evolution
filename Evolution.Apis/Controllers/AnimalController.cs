using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.PlantAggregate;
using Evolution.Services;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private IAnimalsService Service { get; }

        public AnimalController(IAnimalsService service)
        {
            Service = service;
        }
        
        [HttpGet]
        public async Task<Animal> Get(Guid id) => await Service.Get(id);

        [HttpPost]
        public async Task Post(string name) => await Service.CreateNew(name);

        [HttpPut]
        public async Task Put([FromBody] Guid id) => await Service.Act(id);

    }
}
