namespace Evolution.Dtos
{
    public class AnimalDefaultsDto
    {
        public double MinSpeed { get; set; } = 0.1; // 1 action every 10 seconds
        public double MaxSpeed { get; set; } = 10; // 10 actions per sec
        public double Speed { get; set; } = 1; // 1 action per sec
        public double MinEnergy { get; set; } = 1; 
        public double MaxEnergy { get; set; } = 100; // 50 action
        public double Energy { get; set; } = 100; // 50 action
        public int MinFoodStorageCapacity { get; set; } = 1;
        public int MaxFoodStorageCapacity { get; set; } = 100;
        public int FoodStorageCapacity { get; set; } = 3;
        public int OneFoodToEnergy { get; set; } = 20; // 1 food = 20 energy = 10 actions
        public uint SpeedMutationAmplitude { get; set; } = 1;
        public int AdulthoodAge { get; set; } = 30;
        public int Sense { get; set; } = 1; // animal can see locations around him

        public int MinSense { get; set; } = 0;
        public int MaxSense { get; set; } = 1000;
        public uint SenseMutationAmplitude { get; set; } = 1;
    }
}
