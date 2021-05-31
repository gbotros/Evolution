using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;
using Evolution.Domain.PlantAggregate;
using Evolution.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Evolution.Domain.Tests.AnimalTests
{
    public class ActTests
    {
        private DateTime now;

        public ActTests()
        {
            now = DateTime.UtcNow;
        }

        [Fact]
        public void Act_Move()
        {
            // setup mocks

            // arrange
            var worldWidth = 2;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var location2 = new Location(0, 1, worldWidth, worldHeight);
            var initialEnergy = 1000;
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, now, true, initialEnergy, 100, 1, null);

            // act
            animal.Act(now);

            // assert
            Assert.True(animal.Location == location2);
            Assert.True(animal.Energy < initialEnergy);
            Assert.Equal(1, animal.Steps);
        }

        [Fact]
        public void Act_Move_With_No_Place_Available()
        {
            // setup mocks

            // arrange
            var worldWidth = 1;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var initialEnergy = 1000;
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, now, true, initialEnergy, 100, 1, null);

            // act
            animal.Act(now);

            // assert
            Assert.True(animal.Location == location);
            Assert.True(animal.Energy < initialEnergy);
            Assert.Equal(0, animal.Steps);
        }

        [Fact]
        public void Act_move_Starve_Die()
        {
            // setup mocks

            // arrange
            var worldWidth = 1;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var initialEnergy = 1_00_000;
            var speed = 500;
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, now, true, initialEnergy, 100, speed, null);

            // act
            var stepsNeededToDieWithoutFood = 100;
            for (int i = 1; i <= stepsNeededToDieWithoutFood; i++)
            {
                Assert.True(animal.IsAlive);
                animal.Act(now);
            }

            // assert
            Assert.True(animal.Energy == 0);
            Assert.False(animal.IsAlive);
        }

        [Fact]
        public void Act_Move_Eat()
        {
            // setup mocks

            // arrange
            var worldWidth = 2;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var food = new List<IPlantFood>();
            for (int i = 0; i < 3; i++)
            {
                var p = new Plant(Guid.NewGuid(), $"tree {i}", location, null, now);
                food.Add(p);
            }
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, now, true, 1000, 100, 1, null);
            var initialFoodAmount = food.Sum(f=> f.Weight);

            var animalFactory = new AnimalsesFactory(new Mock<IGameCalender>().Object, new Mock<ILocationService>().Object);
            animalFactory.Initialize(animal, food);

            // act
            animal.Act(now);
            var foodWeight = food.Sum(f => f.Weight);

            // assert
            Assert.True(animal.IsAlive);
            Assert.True(animal.StoredFood > 0);
            Assert.True(foodWeight < initialFoodAmount);
        }

    }
}
