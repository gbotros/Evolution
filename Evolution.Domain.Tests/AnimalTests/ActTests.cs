using System;
using System.Collections.Generic;
using Evolution.Domain.Common;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Evolution.Domain.Tests.AnimalTests
{
    public class ActTests
    {
        Mock<ILogger<Animal>> animalLoggerMock;
        Mock<ILogger<Plant>> plantLoggerMock;
        Mock<IGameCalender> calenderMock;

        public ActTests()
        {
            animalLoggerMock = new Mock<ILogger<Animal>>();
            plantLoggerMock = new Mock<ILogger<Plant>>();
            calenderMock = new Mock<IGameCalender>();

        }

        [Fact]
        public void Act_Move()
        {
            // setup mocks
            calenderMock.Setup(c => c.Now).Returns(DateTime.UtcNow);

            // arrange
            var worldWidth = 2;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var location2 = new Location(0, 1, worldWidth, worldHeight);
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, null, null, null, calenderMock.Object, animalLoggerMock.Object);
            var initialEnegry = animal.Energy;

            // act
            animal.Act();

            // assert
            Console.WriteLine(animal.Location.Name);
            Assert.True(animal.Location == location2);
            Assert.True(animal.Energy < initialEnegry);
            Assert.Equal(1, animal.Steps);
        }

        [Fact]
        public void Act_Move_With_No_Place_Available()
        {
            // setup mocks
            calenderMock.Setup(c => c.Now).Returns(DateTime.UtcNow);

            // arrange
            var worldWidth = 1;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, null, null, null, calenderMock.Object, animalLoggerMock.Object);
            var initialEnegry = animal.Energy;

            // act
            animal.Act();

            // assert
            Assert.True(animal.Location == location);
            Assert.True(animal.Energy < initialEnegry);
            Assert.Equal(0, animal.Steps);
        }

        [Fact]
        public void Act_move_Starve_Die()
        {
            // setup mocks
            calenderMock.Setup(c => c.Now).Returns(DateTime.UtcNow);

            // arrange
            var worldWidth = 1;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, null, null, null, calenderMock.Object, animalLoggerMock.Object);

            // act
            var stepsNeededToDieWithoutFood = 100;
            for (int i = 1; i <= stepsNeededToDieWithoutFood; i++)
            {
                Assert.True(animal.IsAlive);
                animal.Act();
            }

            // assert
            Assert.True(animal.Energy == 0);
            Assert.False(animal.IsAlive);
        }

        [Fact]
        public void Act_Move_Eat()
        {
            // setup mocks
            calenderMock.Setup(c => c.Now).Returns(DateTime.UtcNow);

            // arrange
            var worldWidth = 2;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var food_1 = new Plant(Guid.NewGuid(), "tree 1", location, null, calenderMock.Object, plantLoggerMock.Object);
            var food = new List<IPlantFood>() { food_1 };
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, food, null, null, calenderMock.Object, animalLoggerMock.Object);

            // act
            var stepsNeededToBeDieWithoutFood = 100;
            var extraSteps = 10;
            for (int i = 1; i <= stepsNeededToBeDieWithoutFood + extraSteps; i++)
            {
                Assert.True(animal.IsAlive);
                animal.Act();
            }

            // assert
            Assert.True(animal.Energy > 0);
            Assert.True(animal.IsAlive);
            Assert.True(animal.Steps > stepsNeededToBeDieWithoutFood);
            Assert.Equal(0, food_1.Weight);
        }

    }
}
