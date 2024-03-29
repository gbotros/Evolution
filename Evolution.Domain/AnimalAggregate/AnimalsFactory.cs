﻿using System;
using System.Collections.Generic;
using Evolution.Domain.Common;
using Evolution.Domain.GameSettingsAggregate;

namespace Evolution.Domain.AnimalAggregate
{
    public class AnimalsFactory : IAnimalsFactory
    {
        private IGameCalender GameCalender { get; }
        private ILocationService LocationService { get; }

        public AnimalsFactory(IGameCalender gameCalender, ILocationService locationService)
        {
            GameCalender = gameCalender;
            LocationService = locationService;
        }

        public Animal CreateNew(string name, GameSettings settings)
        {
            var id = Guid.NewGuid();
            var location = LocationService.GetRandom(settings.WorldSize);
            var now = GameCalender.Now;

            return new Animal(
                id,
                null,
                name,
                location,
                now,
                true,
                settings,
                settings.AnimalDefaults.MinSpeed,
                settings.AnimalDefaults.MaxSpeed,
                settings.AnimalDefaults.Speed,
                settings.AnimalDefaults.SpeedMutationAmplitude,
                settings.AnimalDefaults.MinEnergy,
                settings.AnimalDefaults.MaxEnergy,
                settings.AnimalDefaults.Energy,
                settings.AnimalDefaults.MinFoodStorageCapacity,
                settings.AnimalDefaults.MaxFoodStorageCapacity,
                settings.AnimalDefaults.FoodStorageCapacity,
                settings.AnimalDefaults.OneFoodToEnergy,
                settings.AnimalDefaults.AdulthoodAge,
                settings.AnimalDefaults.Sense,
                settings.AnimalDefaults.MinSense,
                settings.AnimalDefaults.MaxSense,
                settings.AnimalDefaults.SenseMutationAmplitude
            );
        }

        public Animal CreateNew(
            string name,
            Guid? parentId,
            Location location,
            GameSettings settings,
            double energy,
            int minFoodStorageCapacity,
            int maxFoodStorageCapacity,
            int foodStorageCapacity,
            double speed,
            int oneFoodToEnergy,
            int adulthoodAge,
            double minSpeed,
            double maxSpeed,
            uint speedMutationAmplitude,
            double minEnergy,
            double maxEnergy,
            int sense,
            int minSense,
            int maxSense,
            uint senseMutationAmplitude)
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
                settings,
                minSpeed,
                maxSpeed,
                speed,
                speedMutationAmplitude,
                minEnergy,
                maxEnergy,
                energy,
                minFoodStorageCapacity,
                maxFoodStorageCapacity,
                foodStorageCapacity,
                oneFoodToEnergy,
                adulthoodAge,
                sense,
                minSense,
                maxSense,
                senseMutationAmplitude);
        }

        public void Initialize(Animal animal, IReadOnlyCollection<IFood> food)
        {
            animal.Food = food;
        }
    }
}