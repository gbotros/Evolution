using System.Threading.Tasks;
using System.Timers;
using Evolution.Web.Models;
using Evolution.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Evolution.Web.Shared
{
    public partial class WorldGrid : ComponentBase
    {
        public string newAnimalName;

        [Inject]
        private IAnimalsService AnimalsService { get; set; }
        [Inject]
        private IPlantsService PlantsService { get; set; }
        [Inject]
        private IWorldSizeService WorldSizeService { get; set; }
        [Inject]
        private WorldStore WorldStore { get; set; }

        protected override async Task OnInitializedAsync()
        {
            WorldStore.IsLoading = true;
            WorldStore.WorldSize = await WorldSizeService.GetWorldSize();
            await ReloadAnimals();
            await ReloadPlants();
            WorldStore.IsLoading = false;
        }

        public async Task CreateNewAnimal()
        {
            await AnimalsService.CreateNew(newAnimalName);
            await ReloadAnimals();
        }
        
        public async Task CreateNewPlant()
        {
            await PlantsService.CreateNew();
            await ReloadAnimals();
            await ReloadPlants();
        }

        private void AutoAct()
        {

            var timer = new Timer(10000);
            timer.Enabled = !timer.Enabled;
            timer.Elapsed += async (object source, ElapsedEventArgs e) =>
            {
                var animals = WorldStore.GetAllLiveAnimals();
                foreach (var animal in animals)
                {
                    await AnimalsService.Act(animal.Id);
                    await ReloadAnimals();
                    await InvokeAsync(StateHasChanged);
                    await Task.Delay(500);
                }
                //var plants = WorldStore.GetAllLivePlants();
                //foreach (var plant in plants)
                //{
                //    await PlantsService.Act(plant.Id);
                //    await ReloadPlants();
                //    await InvokeAsync(StateHasChanged);
                //}
            };
        }

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
    }
}
