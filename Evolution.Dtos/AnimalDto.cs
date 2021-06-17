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

        public int MinSpeed { get; set; }
        public int MaxSpeed { get; set; }
        public int Speed { get; set; }
        public int Steps { get; set; }

        public int StoredFood { get; set; }
        public int FoodStorageCapacity { get; set; }

        public int MinEnergy { get; set; }
        public int MaxEnergy { get; set; }
        public int Energy { get; set; }
        public DateTime LastAction { get; set; }
        public DateTime NextAction { get; set; }

        public DateTime LastChildAt { get; set; }
    }
}
