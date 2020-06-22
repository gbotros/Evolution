using System;

namespace Evolution.Apis.Dtos
{
    public class PlantsFilter
    {
        public Guid? Id { get; set; }
        public int? LocationX { get; set; }
        public int? LocationY { get; set; }
    }
}