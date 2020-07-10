using System;

namespace Evolution.Apis.Dtos
{
    public class UpdatePlantDto
    {
        public int Amount { get; set; }
        public Guid Id { get; set; }
        public bool? IsAlive { get; set; }
    }
}