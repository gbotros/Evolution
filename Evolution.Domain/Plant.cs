using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Evolution.Domain
{
    public class Plant : Creature
    {

        private int DefaultGrowthAmount = 10;

        public Plant(Guid id,
            string name,
            Location location,
            IReadOnlyCollection<Creature> creaturesWithinVisionLimit,
            Guid? parentId,
            IGameCalender calender,
            ILogger<Plant> logger)
            : base(
              id,
              name,
              location,
              creaturesWithinVisionLimit,
              parentId,
              calender,
              logger)
        {
            GrowthAmount = DefaultGrowthAmount;
            Weight = DefaultGrowthAmount;
        }

        private int GrowthAmount { get; }

        public override bool IsEatableBy(Type otherType)
        {
            return otherType == typeof(Animal);
        }

        public override void Act()
        {
            if (!IsAlive) return;
            Grow();
        }

        public override int EatInto(int desiredAmount)
        {
            if (desiredAmount >= Weight)
            {
                var originalWeight = Weight;
                Weight = 0;
                Die();
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