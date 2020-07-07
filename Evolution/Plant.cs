using System;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Entities;

namespace Evolution
{
    public class Plant : Creature, IPlant
    {
        public Plant(
            PlantBlueprint blueprint,
            IPlantService plantService,
            IGameCalender gameCalender) : base(gameCalender)
        {
            Id = blueprint.Id;
            Name = blueprint.Name;
            Weight = blueprint.Weight;
            BirthDate = blueprint.BirthDate;
            DeathDate = blueprint.DeathDate;
            GrowthAmount = blueprint.GrowthAmount;
            ParentId = blueprint.ParentId;

            PlantService = plantService;
        }

        private int GrowthAmount { get; }

        private IPlantService PlantService { get; }

        public override async Task Act()
        {
            while (IsAlive) await Grow();
        }

        public override async Task<int> EatInto(int neededAmount)
        {
            var response = await PlantService.EatInto(Id, neededAmount);
            Weight = response.CurrentWeight;
            if (Weight <= 0) Die();

            return response.Eaten;
        }

        public PlantBlueprint GetBlueprint()
        {
            return new PlantBlueprint
            {
                Id = Id,
                Name = Name,
                Weight = Weight,
                BirthDate = BirthDate,
                DeathDate = DeathDate,
                GrowthAmount = GrowthAmount,
                ParentId = ParentId,
                UpdatedAt = DateTime.UtcNow,
                IsAlive = IsAlive,
                Location = Location.Blueprint
            };
        }

        private void Die()
        {
            IsAlive = false;
        }

        private async Task Grow()
        {
            Weight += GrowthAmount;
            await PlantService.Update(GetBlueprint());
        }
    }
}