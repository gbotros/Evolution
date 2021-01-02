using System;
using System.Threading.Tasks;

namespace Evolution.Domain
{
    public class Plant : Creature
    {
        public Plant() : base()
        {
        }

        private int GrowthAmount { get; }

        public override bool IsEatableBy(Creature other)
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