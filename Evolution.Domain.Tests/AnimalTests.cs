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

        public AnimalTests()
        {
            loggerMock = new Mock<ILogger<Animal>>();
        }

        [Fact]
        public void AnimalCanMove()
        {
            // arrange
            var worldWidth = 2;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var location2 = new Location(0, 1, worldWidth, worldHeight);
            var animal = new Animal(Guid.NewGuid(), "1st Animal", location, null, loggerMock.Object);
            var initialEnegry = animal.Energy;

            // assert
            animal.Act();

            // act
            Console.WriteLine(animal.Location.Name);
            Assert.True(animal.Location == location2, animal.Location.Name);
            Assert.True(animal.Energy < initialEnegry);
        }

    }
}
