using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Dtos;
using Evolution.Web.Models;
using Evolution.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Evolution.Web.Shared
{
    public partial class AnimalComponent : ComponentBase
    {
        [Parameter]
        public AnimalDto Animal { get; set; }

        [Inject]
        private WorldStore WorldStore { get; set; }

        [Inject]
        private IAnimalsService Connector { get; set; }

        [Parameter]
        public EventCallback<AnimalDto> OnActed { get; set; }

        public async Task Act(Guid id)
        {
            await Connector.Act(id);
            await OnActed.InvokeAsync(Animal);
        }

        public async Task Kill(Guid id)
        {
            await Connector.Kill(id);
            await OnActed.InvokeAsync(Animal);
        }
        
    }
}
