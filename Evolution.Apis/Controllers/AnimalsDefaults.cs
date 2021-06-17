using Microsoft.AspNetCore.Mvc;
using Evolution.Domain.AnimalAggregate;
using Evolution.Dtos;

namespace Evolution.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsDefaultsController : ControllerBase
    {
        private AnimalDefaults AnimalDefaults { get; }

        public AnimalsDefaultsController(AnimalDefaults animalDefaults )
        {
            AnimalDefaults = animalDefaults;
        } 
        [HttpPut]
        public void SetDefaults(AnimalDefaultsDto ad)
        {
            AnimalDefaults.MinEnergy = ad.MinEnergy;
            AnimalDefaults.MaxEnergy = ad.MaxEnergy;
            AnimalDefaults.Energy = ad.Energy;
            AnimalDefaults.MinSpeed = ad.MinSpeed;
            AnimalDefaults.MaxSpeed = ad.MaxSpeed;
            AnimalDefaults.Speed = ad.Speed; 
            AnimalDefaults.SpeedMutationAmplitude = ad.SpeedMutationAmplitude;
            AnimalDefaults.FoodStorageCapacity = ad.FoodStorageCapacity;
            AnimalDefaults.OneFoodToEnergy = ad.OneFoodToEnergy;
            AnimalDefaults.AdulthoodAge = ad.AdulthoodAge;
        }
    }
}
