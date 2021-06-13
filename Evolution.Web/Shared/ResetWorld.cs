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
    public partial class ResetWorld
    {
        [Inject] private IWorldService WorldService { get; set; }
        [Inject] private WorldStore WorldStore { get; set; }

        private async Task ResetTheWorld()
        {
            await WorldService.Reset();
            WorldStore.SetAnimals(new List<AnimalDto>());
            WorldStore.SetPlants(new List<PlantDto>());
            StateHasChanged();
        }

    }
}
