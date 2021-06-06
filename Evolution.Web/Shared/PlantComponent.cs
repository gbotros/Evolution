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
    public partial class PlantComponent : ComponentBase
    {
        [Parameter]
        public PlantDto Plant { get; set; }

        [Inject]
        private WorldStore WorldStore { get; set; }

        [Inject]
        private IPlantsService PlantsService { get; set; }

        [Parameter]
        public EventCallback<PlantDto> OnActed { get; set; }

        public async Task Act(Guid id)
        {
            await PlantsService.Act(id);
            await OnActed.InvokeAsync(Plant);
        }

        public async Task Kill(Guid id)
        {
            await PlantsService.Kill(id);
            await OnActed.InvokeAsync(Plant);
        }
        
    }
}
