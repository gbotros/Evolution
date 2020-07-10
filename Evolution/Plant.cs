using System;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Abstractions.Dtos;
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
            IsAlive = blueprint.IsAlive;

            PlantService = plantService;
        }

        private int GrowthAmount { get; }

        private IPlantService PlantService { get; }

        public override async Task Act()
        {
            if (!IsAlive) return;
            await Grow();
        }

        public override async Task<int> EatInto(int neededAmount)
        {
            var request = new UpdatePlantDto { Id = Id, Amount = -1 * neededAmount, IsAlive = null };
            var response = await PlantService.Update(request);

            if (response.CurrentWeight <= 0) await Die();

            return response.AmountOfChange;
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

        private async Task Die()
        {
            IsAlive = false;
            await PlantService.Update(new UpdatePlantDto { Amount = 0, Id = Id, IsAlive = IsAlive });
        }

        private async Task Grow()
        {
            if (!IsAlive) return;

            Weight += GrowthAmount;
            await PlantService.Update(new UpdatePlantDto { Amount = 0, Id = Id, IsAlive = IsAlive });
        }

    }
}