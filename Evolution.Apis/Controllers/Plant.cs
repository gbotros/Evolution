using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IEnumerable<string> Get()
        {
            return null;
        }
        
        [HttpPost]
        public void Post() => Service.CreateNew();

        [HttpPut]
        public void Put([FromBody] Guid id) => Service.Act(id);

    }
}
