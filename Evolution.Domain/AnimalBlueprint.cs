using System;

namespace Evolution.Domain
{
    public class AnimalBlueprint
    {
        public DateTime BirthDate { get; set; }
        public int ChildrenCount { get; set; } = 0;
        public DateTime? DeathDate { get; set; } = null;
        public int Energy { get; set; } = 6000; // on default speed 6K Energy is enough for 100 steps
        public Guid Id { get; set; } = Guid.Empty;
        public bool IsAlive { get; set; } = true;
        public ILocation Location { get; set; } = null;
        public string Name { get; set; } = "Animal";
        public Guid? ParentId { get; set; } = null;
        public int Speed { get; set; } = 30; // 30 step per game hour
        public int Steps { get; set; } = 0;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int Weight { get; set; } = 10;
    }
}