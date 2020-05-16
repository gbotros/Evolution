using System;
using Evolution.Abstractions;

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
        public bool IsAlive => Weight > 0;
        public ILocation Location { get; set; }

        public string Name { get; }
        public IPlantService PlantService { get; }

        public int Weight { get; private set; }

        public void Act()
        {
            while (IsAlive) Grow();
        }

        private void Grow()
        {
            Weight += Blueprint.GrowthAmount;
        }
    }
}