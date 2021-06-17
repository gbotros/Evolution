
using Microsoft.AspNetCore.Components;

namespace Evolution.Web.Pages
{
    public partial class Index
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private void GoToReset()
        {
            NavigationManager.NavigateTo("reset");
        }
    }
}
