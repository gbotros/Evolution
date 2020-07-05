using System;

namespace Evolution.Entities
{
    public class AnimalBlueprint
    {
        public int BirthDay { get; set; } = 0;
        public int? DeathDay { get; set; } = null;
        public int Energy { get; set; } = 6000; // on default speed 6K Energy is enough for 100 steps
        public Guid Id { get; set; } = Guid.Empty;
        public bool IsAlive { get; set; } = true;
        public LocationBlueprint Location { get; set; } = null;
        public string Name { get; set; } = "Animal";
        public int Speed { get; set; } = 30; // 30 step per game hour
        public int Steps { get; set; }
        public int Weight { get; set; } = 10;
    }
}