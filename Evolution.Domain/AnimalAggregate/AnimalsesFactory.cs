﻿using System;
using System.Collections.Generic;
using Evolution.Domain.Common;
using Evolution.Services;

namespace Evolution.Domain.AnimalAggregate
{
    public class AnimalsesFactory : IAnimalsFactory
    {

        private const int DefaultSpeed = 500;

        // DefaultStepCost = DefaultSpeed * 2;
        // enough for 100 step on default values
        private const int DefaultEnergy = 1_00_000;

        // equal to 200% DefaultEnergy
        // enough for 200 step on default values
        private const int DefaultFoodStorageCapacity = 20;

        private IGameCalender GameCalender { get; }
        private ILocationService LocationService { get; }

        public AnimalsesFactory(IGameCalender gameCalender, ILocationService locationService)
        {
            GameCalender = gameCalender;
            LocationService = locationService;
        }

        public Animal CreateNew(string name)
        {
            var id = Guid.NewGuid();
            var location = LocationService.GetRandom();
            var now = GameCalender.Now;
            var isAlive = true;

            return new Animal(id, name, location, now, isAlive, DefaultEnergy, DefaultFoodStorageCapacity, DefaultSpeed, null);
        }

        public Animal CreateNew(string name, Location location, int energy, int foodStorageCapacity, int speed, Guid? parentId)
        {
            var id = Guid.NewGuid();
            var now = GameCalender.Now;
            var isAlive = true;

            return new Animal(id, name, location, now, isAlive, energy, foodStorageCapacity, speed, parentId);
        }
        
        public void Initialize(Animal animal, IReadOnlyCollection<IPlantFood> food)
        {
            animal.Food = food;
        }
    }
}