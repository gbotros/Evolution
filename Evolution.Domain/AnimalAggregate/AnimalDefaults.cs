namespace Evolution.Domain.AnimalAggregate
{
    public class AnimalDefaults
    {
        public int MinSpeed { get; set; } = 1; // 1 action per hour
        public int MaxSpeed { get; set; } = 3_600 * 5; // 5 actions per sec

        // how many action per hour
        public int Speed { get; set; } = 3_600; // one action per second

        public int MinEnergy { get; set; } = 1;
        public int MaxEnergy { get; set; } = 720_000;
        // DefaultStepCost = Speed * 2;
        // enough for 100 step on default values
        public int Energy { get; set; } = 360_000;

        // equal to 200% Energy
        // enough for 200 step on default values
        public int FoodStorageCapacity { get; set; } = 5;


        //one food enough for 10 step on default values
        public int OneFoodToEnergy { get; set; } = 72_000;

        public uint SpeedMutationAmplitude { get; set; } = 5;

        public int AdulthoodAge { get; set; } = 30; // sec

    }
}