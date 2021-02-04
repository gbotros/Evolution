using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Evolution.Domain.Tests
{
    public class AnimalTests
    {
        Mock<ILogger<Animal>> loggerMock;
        Mock<IGameCalender> calenderMock;

        public AnimalTests()
        {
            loggerMock = new Mock<ILogger<Animal>>();
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
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, null, null, null, calenderMock.Object, loggerMock.Object);
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
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, null, null, null, calenderMock.Object, loggerMock.Object);
            var initialEnegry = animal.Energy;

            // act
            animal.Act();

            // assert
            Assert.True(animal.Location == location);
            Assert.True(animal.Energy < initialEnegry);
            Assert.Equal(0, animal.Steps);
        }

        [Fact]
        public void AnimalWithoutFoodWouldDieOutOfStarvation()
        {
            // setup mocks
            calenderMock.Setup(c => c.Now).Returns(DateTime.UtcNow);

            // arrange
            var worldWidth = 1;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, null, null, null, calenderMock.Object, loggerMock.Object);

            // act
            var stepsNeededToBeDieWithoutFood = 100;
            for (int i = 1; i <= stepsNeededToBeDieWithoutFood; i++)
            {
                Assert.True(animal.IsAlive);
                animal.Act();
            }

            // assert
            Assert.True(animal.Energy == 0);
            Assert.False(animal.IsAlive);
        }




    }
}
