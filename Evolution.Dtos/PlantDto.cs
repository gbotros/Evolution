using System;

namespace Evolution.Dtos
{
    public class PlantDto
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeathTime { get; set; }

        public bool IsAlive { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public int Weight { get; set; }
        public LocationDto Location { get; set; }


    }
}
