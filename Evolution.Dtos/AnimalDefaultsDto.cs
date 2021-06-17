namespace Evolution.Dtos
{
    public class AnimalDefaultsDto
    {
        public int MinSpeed { get; set; } = 1;
        public int MaxSpeed { get; set; } = 3_600 * 5;
        public int Speed { get; set; } = 3_600;
        public int MinEnergy { get; set; } = 1;
        public int MaxEnergy { get; set; } = 720_000;
        public int Energy { get; set; } = 360_000;
        public int FoodStorageCapacity { get; set; } = 5;
        public int OneFoodToEnergy { get; set; } = 72_000;
        public uint SpeedMutationAmplitude { get; set; } = 5;
        public int AdulthoodAge { get; set; } = 30;

    }
}
