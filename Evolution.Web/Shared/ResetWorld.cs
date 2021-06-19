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
        [Inject] private IGameSettingsService GameSettingsService { get; set; }
        [Inject] private WorldStore WorldStore { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        public GameSettingsDto S { get; set; }

        protected override async Task OnInitializedAsync()
        {
            S = WorldStore.GameSettingsDto;
        }

        private async Task ResetTheWorld()
        {
            await ResetAnimalsDefaults();
            StateHasChanged();

            NavigationManager.NavigateTo("/");
        }

        private async Task ResetAnimalsDefaults()
        {
            await GameSettingsService.Reset(S);
        }
    }
}
