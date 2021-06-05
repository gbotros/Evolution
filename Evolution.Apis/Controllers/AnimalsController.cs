﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Domain.AnimalAggregate;
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
        [Route("{id}")]
        public async Task Post(string name) => await Service.CreateNew(name);

        [HttpPut]
        [Route("{id}")]
        public async Task Put([FromRoute] Guid id) => await Service.Act(id);

    }
}
