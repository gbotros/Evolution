using System;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Blueprints;

namespace Evolution
{
    public class Plant : IPlant
    {
        public Plant(PlantBlueprint blueprint, IPlantService plantService)
        {
            Id = blueprint.Id;
            Name = blueprint.Name;
            Weight = blueprint.Weight;
            BirthDay = blueprint.BirthDay;
            DeathDay = blueprint.DeathDay;
            GrowthAmount = blueprint.GrowthAmount;
            Blueprint = blueprint;
            PlantService = plantService;
        }

        public int BirthDay { get; }
        public PlantBlueprint Blueprint { get; }
        public int? DeathDay { get; }

        public int GrowthAmount { get; }
        public Guid Id { get; }

        public bool IsAlive { get; set; }

        public ILocation Location { get; set; }

        public string Name { get; }
        public IPlantService PlantService { get; }

        public int Weight { get; private set; }

        public async Task Act()
        {
            while (IsAlive) await Grow();
        }

        public async Task<int> EatInto(int neededAmount)
        {
            var response = await PlantService.EatInto(Id, neededAmount);
            Weight = response.CurrentWeight;
            if (Weight <= 0) Die();

            return response.Eaten;
        }

        private void Die()
        {
            IsAlive = false;
        }

        private async Task Grow()
        {
            Weight += Blueprint.GrowthAmount;
            await PlantService.Update(Blueprint);
        }
    }
}