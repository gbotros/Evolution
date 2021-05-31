using System;

namespace Evolution.Dtos
{
    public class AnimalDto
    {
        public DateTime CreationTime { get; set; }
        public DateTime? DeathTime { get; set; }

        public bool IsAlive { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public int Weight { get; set; }
        public LocationDto Location { get; set; }

        public int ChildrenCount { get; set; }

        public int Speed { get; set; }
        public int Steps { get; set; }

        public int StoredFood { get; set; }
        public int FoodStorageCapacity { get; set; }

        public int Energy { get; set; }
    }
}
