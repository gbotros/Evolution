using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Evolution.Domain.Tests
{
    public class AnimalTests
    {
        Location location00;
        Location location01;
        Mock<ILogger<Animal>> loggerMock;

        public AnimalTests()
        {
            loggerMock = new Mock<ILogger<Animal>>();
        }

        [Fact]
        public void AnimalCanMove()
        {
            // arrange
            GameSettings.World.Width = 2;
            GameSettings.World.Height = 1;
            var location = new Location(0, 0);
            var location2 = new Location(0, 1);
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, null, loggerMock.Object);

            // assert
            animal.Act();

            // act
            Assert.True(animal.Location == location2);
        }

    }
}
