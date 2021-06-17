using System;
using System.Collections.Generic;
using Evolution.Domain.Common;

namespace Evolution.Domain.AnimalAggregate
{
    public class AnimalsFactory : IAnimalsFactory
    {
        private AnimalDefaults Defaults { get; }
        private IGameCalender GameCalender { get; }
        private ILocationService LocationService { get; }

        public AnimalsFactory(AnimalDefaults defaults, IGameCalender gameCalender, ILocationService locationService)
        {
            Defaults = defaults;
            GameCalender = gameCalender;
            LocationService = locationService;
        }

        public Animal CreateNew(string name)
        {
            var id = Guid.NewGuid();
            var location = LocationService.GetRandom();
            var now = GameCalender.Now;

            return new Animal(
                id,
                null,
                name,
                location,
                now,
                true,
                Defaults.MinSpeed,
                Defaults.MaxSpeed,
                Defaults.Speed,
                Defaults.SpeedMutationAmplitude,
                Defaults.MinEnergy,
                Defaults.MaxEnergy,
                Defaults.Energy,
                Defaults.FoodStorageCapacity,
                Defaults.OneFoodToEnergy,
                Defaults.AdulthoodAge);
        }

        public Animal CreateNew(string name, Location location, int energy, int foodStorageCapacity, int speed, Guid? parentId)
        {
            var id = Guid.NewGuid();
            var now = GameCalender.Now;

            return new Animal(
                id,
                parentId,
                name,
                location,
                now,
                true,
                Defaults.MinSpeed,
                Defaults.MaxSpeed,
                speed,
                Defaults.SpeedMutationAmplitude,
                Defaults.MinEnergy,
                Defaults.MaxEnergy,
                Defaults.Energy,
                Defaults.FoodStorageCapacity,
                Defaults.OneFoodToEnergy,
                Defaults.AdulthoodAge);
        }

        public void Initialize(Animal animal, IReadOnlyCollection<IPlantFood> food)
        {
            animal.Food = food;
        }
    }
}