using System;

namespace Evolution
{
    public class PlantBlueprint
    {
        public int BirthDay { get; set; } = 0;
        public int? DeathDay { get; set; } = null;
        public int GrowthAmount { get; set; } = 1;
        public Guid Id { get; set; }
        public bool IsAlive { get; set; } = true;
        public string Name { get; set; }
        public int Weight { get; set; } = 100;
    }
}