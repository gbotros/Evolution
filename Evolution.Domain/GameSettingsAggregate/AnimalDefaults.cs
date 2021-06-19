using Evolution.Domain.Common;

namespace Evolution.Domain.GameSettingsAggregate
{
    public class AnimalDefaults : ValueObject<AnimalDefaults>
    {
        public double MinSpeed { get; set; }
        public double MaxSpeed { get; set; }
        // how many action per sec
        public double Speed { get; set; }

        public double MinEnergy { get; set; }
        public double MaxEnergy { get; set; }
        public double Energy { get; set; }

        public int FoodStorageCapacity { get; set; }

        public int OneFoodToEnergy { get; set; }

        public uint SpeedMutationAmplitude { get; set; }

        public int AdulthoodAge { get; set; }
        public int Sense { get; set; }

        protected override bool EqualsCore(AnimalDefaults other)
        {
            return
                MinSpeed == other.MinSpeed
                && MaxSpeed == other.MaxSpeed
                && Speed == other.Speed
                && MinEnergy == other.MinEnergy
                && MaxEnergy == other.MaxEnergy
                && Energy == other.Energy
                && FoodStorageCapacity == other.FoodStorageCapacity
                && OneFoodToEnergy == other.OneFoodToEnergy
                && SpeedMutationAmplitude == other.SpeedMutationAmplitude
                && AdulthoodAge == other.AdulthoodAge
                && Sense == other.Sense;
        }

        protected override int GetHashCodeCore()
        {
            return
                $"{MinSpeed}-{MaxSpeed}-{Speed}-{MinEnergy}-{MaxEnergy}-{Energy}-{FoodStorageCapacity}-{OneFoodToEnergy}-{SpeedMutationAmplitude}-{AdulthoodAge}"
                    .GetHashCode();
        }
    }
}