using System;
using System.Threading.Tasks;

namespace Evolution.Domain
{
    public class Plant : Creature, IPlant
    {
        public Plant(
            PlantBlueprint blueprint,
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
        }

        private int GrowthAmount { get; }

        public override bool IsEatableBy(ICreature other)
        {
            return true;
        }

        public override void Act()
        {
            if (!IsAlive) return;
            Grow();
        }

        public override void EatInto(int neededAmount)
        {
            if (Weight - neededAmount <= 0) throw new ApplicationException("not enough amount to be eaten in the plant");

            Weight -= neededAmount;
            if (Weight <= 0) Die();
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
                Location = Location
            };
        }

        private void Die()
        {
            IsAlive = false;
        }

        private void Grow()
        {
            if (!IsAlive) throw new ApplicationException("Dead plants cannot grow");

            Weight += GrowthAmount;
        }

    }
}