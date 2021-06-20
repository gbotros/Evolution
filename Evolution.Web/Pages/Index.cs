
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Evolution.Dtos;
using Evolution.Web.Models;
using Evolution.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Evolution.Web.Pages
{
    public partial class Index
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IAnimalsService AnimalsService { get; set; }
        [Inject] private IPlantsService PlantsService { get; set; }
        [Inject] private IGameSettingsService GameSettingsService { get; set; }
        [Inject] private WorldStore WorldStore { get; set; }

        //private Timer Timer { get; set; } = new(1000);

        public List<AnimalDto> Animals { get; set; } = new();
        public List<PlantDto> Plants { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Init();
            WorldStore.IsLoading = true;
            WorldStore.GameSettingsDto = await GameSettingsService.Get();
            WorldStore.IsLoading = false;
        }

        private void Init()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await ReloadAnimals();
                    await ReloadPlants();
                    StateHasChanged();
                    await Task.Delay(100);
                }
            });
        }

        private async Task ReloadAnimals()
        {
            var after = WorldStore.LastActionTime;
            var animals = await AnimalsService.GetAll(after);
            WorldStore.SetAnimals(animals);
            Animals = WorldStore.GetAnimals();
        }


        private async Task ReloadPlants()
        {
            var plants = await PlantsService.GetAll();
            WorldStore.SetPlants(plants);
            Plants = plants;
        }

        //private async void OnTimerElapsed(object source, ElapsedEventArgs e)
        //{
        //    await ReloadAnimals();
        //    await ReloadPlants();
        //}

        private void GoToReset()
        {
            NavigationManager.NavigateTo("reset");
        }
    }
}
