using System;

namespace Evolution.Dtos
{
    public class AnimalDto
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeathTime { get; set; }

        public bool IsAlive { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public int Weight { get; set; }
        public LocationDto Location { get; set; }

        public int ChildrenCount { get; set; }

        public double MinSpeed { get; set; }
        public double MaxSpeed { get; set; }
        public double Speed { get; set; }
        public int Steps { get; set; }

        public int StoredFood { get; set; }
        public int FoodStorageCapacity { get; set; }

        public double MinEnergy { get; set; }
        public double MaxEnergy { get; set; }
        public double Energy { get; set; }
        public DateTime LastAction { get; set; }
        public DateTime NextAction { get; set; }

        public DateTime LastChildAt { get; set; }
        public int Sense { get; set; }
        public string Direction { get; set; }
    }
}
