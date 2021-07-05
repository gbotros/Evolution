using System;
using System.Threading.Tasks;
using Evolution.Dtos;
using Evolution.Web.Models;
using Evolution.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

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

        private bool statsOpen { get; set; } = false;
        private bool hovering { get; set; } = false;

        private bool showStats => hovering || statsOpen;

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

        private string CalculateActionDelay()
        {
            if (!Animal.IsAlive)
            {
                return "";
            }

            var now = DateTime.UtcNow;
            var velocity = 1d / Animal.Speed;
            var timeSpan = TimeSpan.FromSeconds(velocity);
            var expectedLastAction = now - timeSpan;
            var delay = expectedLastAction - Animal.LastAction;
            var delayInSec = (int)delay.TotalSeconds;

            if (delayInSec <= 0)
            {
                return "";
            }

            return delayInSec.ToString();
        }

        private void OnMouseOut(MouseEventArgs obj)
        {
            hovering = false;
        }

        private void OnMouseOver(MouseEventArgs obj)
        {
            hovering = true;
        }
        
        private void CloseStats(MouseEventArgs obj)
        {
            statsOpen = false;
        }

        private void OnClick(MouseEventArgs obj)
        {
            statsOpen = true;
        }
    }
}
