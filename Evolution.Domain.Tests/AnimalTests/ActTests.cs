using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;
using Evolution.Domain.GameSettingsAggregate;
using Evolution.Domain.PlantAggregate;
using Moq;
using Xunit;

namespace Evolution.Domain.Tests.AnimalTests
{
    public class ActTests
    {
        private DateTime now;
        private GameSettings settings;

        public ActTests()
        {
            now = DateTime.UtcNow;
            settings = new GameSettings(Guid.NewGuid(), new WorldSize(5, 5), new AnimalDefaults());
        }

        [Fact]
        public void Act_Move()
        {
            // setup mocks

            // arrange
            var worldSize = new WorldSize(2, 1);
            settings = new GameSettings(Guid.NewGuid(), worldSize, new AnimalDefaults());
            var location = new Location(0, 0);
            var location2 = new Location(0, 1);
            var initialEnergy = 1000;
            var animal = new Animal(Guid.NewGuid(),
                null,
                "1st Animal",
                location,
                now,
                true,
                settings,
                1,
                100,
                100,
                0,
                1,
                10_000,
                initialEnergy,
                1,
                1,
                60,
                1);

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
            // arrange
            var worldSize = new WorldSize(1, 1);
            settings = new GameSettings(Guid.NewGuid(), worldSize, new AnimalDefaults());
            var location = new Location(0, 0);
            var initialEnergy = 1000;
            var animal = new Animal(
                Guid.NewGuid(),
                null,
                "1st Animal",
                location,
                now,
                true,
                settings,
                1,
                100,
                100,
                0,
                1,
                1000,
                initialEnergy,
                1,
                1,
                60,
                1);

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
            var worldSize = new WorldSize(1, 1);
            var location = new Location(0, 0);
            var initialEnergy = 1_00_000;
            var speed = 500;
            var animal = new Animal(
                Guid.NewGuid(),
                null,
                "1st Animal",
                location,
                now,
                true,
                settings,
                1,
                100,
                speed,
                1,
                1,
                1000,
                initialEnergy,
                1,
                1,
                60,
                1);

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
            var worldSize = new WorldSize(2, 1);
            var location = new Location(0, 0);
            var food = new List<IFood>();
            for (int i = 0; i < 3; i++)
            {
                var p = new Plant(Guid.NewGuid(), $"tree {i}", location, null, now);
                food.Add(p);
            }
            var animal = new Animal(
                Guid.NewGuid(),
                null,
                "1st Animal",
                location,
                now,
                true,
                settings,
                1,
                100,
                100,
                1,
                1,
                1000,
                1000,
                10,
                1,
                60,
                1);
            var initialFoodAmount = food.Sum(f => f.Weight);

            var animalFactory = new AnimalsFactory(new Mock<IGameCalender>().Object, new Mock<ILocationService>().Object);
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
