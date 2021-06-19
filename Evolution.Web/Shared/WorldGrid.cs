using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Evolution.Dtos;
using Evolution.Web.Models;
using Evolution.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Evolution.Web.Shared
{
    public partial class WorldGrid : ComponentBase
    {
        public string newAnimalName;

        [Inject] private IAnimalsService AnimalsService { get; set; }
        [Inject] private IPlantsService PlantsService { get; set; }
        [Inject] private IGameSettingsService GameSettingsService { get; set; }
        [Inject] private WorldStore WorldStore { get; set; }

        private bool autoPlayMode { get; set; } = false;
        private Timer Timer { get; set; } = new(1000);

        protected override async Task OnInitializedAsync()
        {
            WorldStore.IsLoading = true;
            WorldStore.GameSettingsDto = await GameSettingsService.Get();
            await ReloadAnimals();
            await ReloadPlants();
            WorldStore.IsLoading = false;

            Timer.Enabled = true;
            Timer.Elapsed += OnTimerElapsed;
        }

        public async Task CreateNewAnimal()
        {
            await AnimalsService.CreateNew(newAnimalName ?? "Animal");
            await ReloadAnimals();
        }

        public async Task CreateNewPlant()
        {
            await PlantsService.CreateNew();
            await ReloadAnimals();
            await ReloadPlants();
        }

        //private async Task AutoPlay()
        //{
        //    autoPlayMode = !autoPlayMode;
        //    plantsTimer.Enabled = autoPlayMode;
        //    if (!autoPlayMode) return;

        //    List<AnimalDto> animals;
        //    do
        //    {
        //        animals = WorldStore.GetAllLiveAnimals();
        //        if (!animals.Any()) break;

        //        foreach (var animal in animals)
        //        {
        //            await AnimalsService.Act(animal.Id);
        //            await Task.Delay(10);
        //        }
        //        await ReloadAnimals();
        //        await InvokeAsync(StateHasChanged);
        //    } while (animals.Any() && autoPlayMode);

        //}        //private async Task AutoPlay()
        //{
        //    autoPlayMode = !autoPlayMode;
        //    plantsTimer.Enabled = autoPlayMode;
        //    if (!autoPlayMode) return;

        //    List<AnimalDto> animals;
        //    do
        //    {
        //        animals = WorldStore.GetAllLiveAnimals();
        //        if (!animals.Any()) break;

        //        foreach (var animal in animals)
        //        {
        //            await AnimalsService.Act(animal.Id);
        //            await Task.Delay(10);
        //        }
        //        await ReloadAnimals();
        //        await InvokeAsync(StateHasChanged);
        //    } while (animals.Any() && autoPlayMode);

        //}

        private async Task ReloadAnimals()
        {
            var animals = await AnimalsService.GetAll();
            WorldStore.SetAnimals(animals);
            StateHasChanged();
        }

        private async Task ReloadPlants()
        {
            var plants = await PlantsService.GetAll();
            WorldStore.SetPlants(plants);
            StateHasChanged();
        }

        private async void OnPlantsTimerOnElapsed(object source, ElapsedEventArgs e)
        {
            var plants = WorldStore.GetAllLivePlants();
            if (!plants.Any())
            {
                await CreateNewPlant();
                await CreateNewPlant();
            }
            foreach (var plant in plants)
            {
                await PlantsService.Act(plant.Id);
                await InvokeAsync(StateHasChanged);
            }

            await ReloadPlants();
        }

        private async void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            await ReloadAnimals();
            await ReloadPlants();
        }
    }
}
