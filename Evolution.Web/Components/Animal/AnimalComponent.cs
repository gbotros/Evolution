using System;
using System.Threading.Tasks;
using Evolution.Dtos;
using Evolution.Web.Models;
using Evolution.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Evolution.Web.Components.Animal
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

        private double GetEnergyPercentage()
        {
            return Animal.Energy * 100 / Animal.MaxEnergy;
        }

        private string GetEnergyPercentageColor()
        {
            var percentage = GetEnergyPercentage();
            if (percentage > 30) return "bg-green-300";
            if (percentage > 10) return "bg-yellow-300";
            return "bg-red-300";
        }
    }
}
