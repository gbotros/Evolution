using System;

namespace Evolution.Apis.Dtos
{
    public class AnimalsFilter
    {
        public Guid Id { get; set; }
        public int? LocationX { get; set; }
        public int? LocationY { get; set; }
    }
}