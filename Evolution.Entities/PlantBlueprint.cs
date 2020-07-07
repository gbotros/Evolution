using System;

namespace Evolution.Entities
{
    public class PlantBlueprint
    {
        public DateTime BirthDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeathDate { get; set; } = null;
        public int GrowthAmount { get; set; } = 1;
        public Guid Id { get; set; } = Guid.Empty;
        public bool IsAlive { get; set; } = true;
        public LocationBlueprint Location { get; set; } = null;
        public string Name { get; set; } = "Plant";
        public Guid? ParentId { get; set; } = null;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int Weight { get; set; } = 100;
    }
}