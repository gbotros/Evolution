using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Evolution.Dtos;
using Evolution.Web.Models;
using Evolution.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Evolution.Web.Components.WorldGrid
{
    public partial class WorldGrid : ComponentBase
    {
        public string newAnimalName;

        [Inject] private IAnimalsService AnimalsService { get; set; }
        [Inject] private IPlantsService PlantsService { get; set; }
        [Inject] private WorldStore WorldStore { get; set; }

        [Parameter] public List<AnimalDto> Animals { get; set; }
        [Parameter] public List<PlantDto> Plants { get; set; }

        protected override async Task OnInitializedAsync()
        {

        }

        public async Task CreateNewAnimal()
        {
            await AnimalsService.CreateNew(newAnimalName ?? "A");
        }

        public async Task CreateNewPlant()
        {
            await PlantsService.CreateNew();
        }


        private IEnumerable<PlantDto> GetPlantsAt(int row, int col)
        {
            return WorldStore.GetPlantsAt(row, col);
        }

        private IEnumerable<AnimalDto> GetAnimalsAt(int row, int col)
        {
            return WorldStore.GetAnimalsAt(row, col);
        }
    }
}
