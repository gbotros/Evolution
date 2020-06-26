using System;

namespace Evolution.Entities
{
    public class PlantBlueprint
    {
        public int BirthDay { get; set; } = 0;
        public int? DeathDay { get; set; } = null;
        public int GrowthAmount { get; set; } = 1;
        public Guid Id { get; set; } = Guid.Empty;
        public bool IsAlive { get; set; } = true;
        public LocationBlueprint Location { get; set; } = null;
        public string Name { get; set; } = "Plant";
        public int Weight { get; set; } = 100;
    }
}