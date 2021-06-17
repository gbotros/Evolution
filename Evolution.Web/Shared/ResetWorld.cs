using System.Collections;
using System.Collections.Generic;
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
        [Inject] private IAnimalsDefaultsService AnimalsDefaultsService { get; set; }
        [Inject] private WorldStore WorldStore { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        public AnimalDefaultsDto D { get; } = new AnimalDefaultsDto();

        private async Task ResetTheWorld()
        {
            await DeleteAllCreatures();
            await ResetAnimalsDefaults();
            StateHasChanged();

            NavigationManager.NavigateTo("/");
        }

        private async Task DeleteAllCreatures()
        {
            await WorldService.Reset();
            WorldStore.SetAnimals(new List<AnimalDto>());
            WorldStore.SetPlants(new List<PlantDto>());
        }

        private async Task ResetAnimalsDefaults()
        {
            await AnimalsDefaultsService.Reset(D);
        }
    }
}
