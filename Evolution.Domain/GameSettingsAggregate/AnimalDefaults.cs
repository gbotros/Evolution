using Evolution.Domain.Common;

namespace Evolution.Domain.GameSettingsAggregate
{
    public class AnimalDefaults : ValueObject<AnimalDefaults>
    {
        public int MinSpeed { get; set; }
        public int MaxSpeed { get; set; }
        // how many action per hour
        public int Speed { get; set; }

        public int MinEnergy { get; set; }
        public int MaxEnergy { get; set; }
        public int Energy { get; set; }

        public int FoodStorageCapacity { get; set; }

        public int OneFoodToEnergy { get; set; }

        public uint SpeedMutationAmplitude { get; set; }

        public int AdulthoodAge { get; set; }

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
                && AdulthoodAge == other.AdulthoodAge;
        }

        protected override int GetHashCodeCore()
        {
            return
                $"{MinSpeed}-{MaxSpeed}-{Speed}-{MinEnergy}-{MaxEnergy}-{Energy}-{FoodStorageCapacity}-{OneFoodToEnergy}-{SpeedMutationAmplitude}-{AdulthoodAge}"
                    .GetHashCode();
        }
    }
}