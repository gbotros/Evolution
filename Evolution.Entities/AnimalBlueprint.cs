using System;

namespace Evolution.Entities
{
    public class AnimalBlueprint
    {
        public int BirthDay { get; set; } = 0;
        public int? DeathDay { get; set; } = null;
        public int Energy { get; set; } = 10000; // on default speed 10K Energy is enough for 10 steps
        public Guid Id { get; set; } = Guid.Empty;
        public bool IsAlive { get; set; } = true;
        public LocationBlueprint Location { get; set; } = null;
        public string Name { get; set; } = "Animal";
        public int Speed { get; set; } = 500; // 500 step per second
        public int Weight { get; set; } = 10;
    }
}