using System;
using Evolution.Domain.Common;
using Evolution.Domain.PlantAggregate;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Evolution.Domain.Tests.PlantTests
{
    public class EatIntoTests
    {
        Mock<ILogger<Plant>> plantLoggerMock;
        DateTime now;

        public EatIntoTests()
        {
            plantLoggerMock = new Mock<ILogger<Plant>>();
            now = DateTime.UtcNow;
        }

        [Fact]
        public void EatInto_DeadPlant_NoChange()
        {
            // arrange
            var location = new Location(0, 0);
            var plant = new Plant(Guid.NewGuid(), "p1", location, null, now);

            var fullWeight = plant.Weight;

            plant.EatInto(fullWeight);

            Assert.Equal(0, plant.Weight);
            Assert.False(plant.IsAlive);

            // act
            var actual = plant.EatInto(plant.Weight);

            // assert
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EatInto_LivePlant_WeightReduced()
        {
            // arrange
            var location = new Location(0, 0);
            var plant = new Plant(Guid.NewGuid(), "p1", location, null, now);


            var fullWeight = plant.Weight;
            var eatAmount = 3;
            var expectedWeight = fullWeight - eatAmount;

            // act
            var actualEaten = plant.EatInto(eatAmount);
            var actualWeight = plant.Weight;

            // assert
            Assert.True(plant.IsAlive);
            Assert.Equal(expectedWeight, actualWeight);
            Assert.Equal(eatAmount, actualEaten);
        }

    }
}
