using System;

namespace Evolution.Apis
{
    public class Creatures
    {
        public int BirthMin { get; set; }
        public int? DeathMin { get; set; }
        public Guid Id { get; set; }
        public bool IsAlive { get; set; }
        public string LocationName { get; set; }
        public string Name { get; set; }
        public string Speed { get; set; }
        public int Steps { get; set; }
        public string Type { get; set; }
        public int Weight { get; set; }
    }
}