using System;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Evolution.Domain.Tests.PlantTests
{
    public class IsEatableByTests
    {
        Mock<ILogger<Plant>> plantLoggerMock;
        Mock<IGameCalender> calenderMock;

        public IsEatableByTests()
        {
            plantLoggerMock = new Mock<ILogger<Plant>>();
            calenderMock = new Mock<IGameCalender>();
        }

        [Fact]
        public void IsEatableBy_Animal_True()
        {
            // arrange
            var worldWidth = 1;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var plant = new Plant(Guid.NewGuid(), "p1", location, null, calenderMock.Object, plantLoggerMock.Object);

            // act
            var actual = plant.IsEatableBy(typeof(Animal));

            // assert
            Assert.True(actual);
        }

        [Fact]
        public void IsEatableBy_Plant_False()
        {
            // arrange
            var worldWidth = 1;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var plant = new Plant(Guid.NewGuid(), "p1", location, null, calenderMock.Object, plantLoggerMock.Object);

            // act
            var actual = plant.IsEatableBy(typeof(Plant));

            // assert
            Assert.False(actual);
        }

        [Fact]
        public void IsEatableBy_AnyObject_False()
        {
            // arrange
            var worldWidth = 1;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var plant = new Plant(Guid.NewGuid(), "p1", location, null, calenderMock.Object, plantLoggerMock.Object);

            // act
            var actual = plant.IsEatableBy(typeof(object));

            // assert
            Assert.False(actual);
        }
    }
}
