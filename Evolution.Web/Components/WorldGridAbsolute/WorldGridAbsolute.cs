using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Dtos;
using Evolution.Web.Models;
using Evolution.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Evolution.Web.Components.WorldGridAbsolute
{
    public partial class WorldGridAbsolute : ComponentBase
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



    }
}
