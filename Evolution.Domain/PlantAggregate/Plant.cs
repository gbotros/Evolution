using System;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;
using Evolution.Domain.GameSettingsAggregate;

namespace Evolution.Domain.PlantAggregate
{
    public class Plant : AggregateRoot, IPlantFood
    {

        private int DefaultGrowthAmount = 10;

        public Plant(Guid id,
            string name,
            Location location,
            Guid? parentId,
            DateTime creationTime)
            : base(id)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ApplicationException("Name can't be empty");

            Name = name;
            Location = location;
            ParentId = parentId;
            IsAlive = true;

            CreationTime = creationTime;
            GrowthAmount = DefaultGrowthAmount;
            Weight = DefaultGrowthAmount;

        }

        protected Plant()
        {

        }

        public double GetAge(DateTime now)
        {
            return (now - CreationTime).TotalSeconds;
        }

        public DateTime CreationTime { get; private set; }
        public DateTime? DeathTime { get; private set; }

        public bool IsAlive { get; private set; }
        public string Name { get; private set; }
        public Guid? ParentId { get; private set; }
        public int Weight { get; private set; }
        public Location Location { get; private set; }
        public int GrowthAmount { get; private set; }
        public GameSettings Settings { get; set; }

        public bool IsEatableBy(Type otherType)
        {
            return otherType == typeof(Animal);
        }

        public void Act()
        {
            if (!IsAlive) return;
            Grow();
        }

        public int EatInto(int desiredAmount)
        {
            if (desiredAmount >= Weight)
            {
                var originalWeight = Weight;
                Weight = 0;
                // Die(); // TODO: check later if I want plant to die on weight zero
                return originalWeight;
            }
            else
            {
                Weight -= desiredAmount;
                return desiredAmount;
            }
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