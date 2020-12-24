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
        AnimalBlueprint simpleAnimalBluePrint;
        Mock<IGameCalender> gameCalenderMock;
        Mock<ILogger<Animal>> loggerMock;

        public AnimalTests()
        {
             BuildLocationsGrid();
            simpleAnimalBluePrint = GetSimpleAnimal();
            gameCalenderMock = new Mock<IGameCalender>();
            loggerMock = new Mock<ILogger<Animal>>();
        }

        [Fact]
        public void AnimalCanMove()
        {
            // arrange
            var animal = new Animal(simpleAnimalBluePrint, location00, gameCalenderMock.Object, loggerMock.Object);

            // assert
            animal.Act();

            // act
            Assert.True(animal.Location == location01);
            Assert.True(location00.Community.Count == 0);
            Assert.True(location01.Community.Count == 1);
        }

        private AnimalBlueprint GetSimpleAnimal()
        {
            return new AnimalBlueprint()
            {
                Id = Guid.NewGuid(),
                Name = "1st Animal",
                Energy = 20000
            };
        }

        private void BuildLocationsGrid()
        {
            location00 = new Location(0, 1, new List<ICreature>(), new List<ILocation>());
            location01 = new Location(1, 1, new List<ICreature>(), new List<ILocation>());

            location00.AddNeighbor(location01);
            location01.AddNeighbor(location00);
        }
    }
}
