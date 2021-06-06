using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Evolution.Dtos;
using Microsoft.AspNetCore.Components;

namespace Evolution.Web.Shared
{
    public partial class AnimalsAdminDashBoard : ComponentBase
    {

        public List<AnimalDto> animals = new();
        public string newAnimalName;

        [Inject]
        private HttpClient Http { get; set; }
        private const string animalsUrl = "https://localhost:6001/apis/animals";

        protected override async Task OnInitializedAsync()
        {
            await ReLoadAnimals();
        }

        public async Task CreateNew()
        {
            var response = await Http.PostAsJsonAsync($"{animalsUrl}", newAnimalName);
            await ReLoadAnimals();
            await ReLoadAnimals();
        }

        public async Task Kill(Guid id)
        {
            await Http.DeleteAsync($"{animalsUrl}/{id}");
            await ReLoadAnimals();
        }

        public async Task Act(Guid id)
        {
            await Http.PutAsJsonAsync($"{animalsUrl}/{id}", "");
            await ReLoadAnimals();
        }

        private async Task ReLoadAnimals()
        {
            animals = await Http.GetFromJsonAsync<List<AnimalDto>>(animalsUrl);
        }
    }
}
