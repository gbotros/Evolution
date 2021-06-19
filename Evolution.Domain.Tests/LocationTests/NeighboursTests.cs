using System.Linq;
using Evolution.Domain.Common;
using Evolution.Domain.GameSettingsAggregate;
using Xunit;

namespace Evolution.Domain.Tests.LocationTests
{
    public class NeighboursTests
    {

        [Fact]
        public void NeighboursInOneCellWorld()
        {
            // arrange
            var worldSize = new WorldSize(1, 1);
            var location = new Location(0, 0);

            // set expectation
            var expectedNeighboursCount = 0;

            // act
            var neighbours = location.GetNeighbours(worldSize);

            // assert
            Assert.NotNull(neighbours);
            Assert.Equal(expectedNeighboursCount, neighbours.Count);
        }

        [Fact]
        public void NeighboursInTwoCellWorld()
        {
            // arrange
            var worldSize = new WorldSize(2, 1);
            var location = new Location(0, 0 );
            var location01 = new Location(0, 1 );

            // set expectation
            var excpectedNeighboursCount = 1;

            // act
            var neighbours = location.GetNeighbours(worldSize);

            // assert
            Assert.NotNull(neighbours);
            Assert.Equal(excpectedNeighboursCount, neighbours.Count);
            Assert.Equal(location01, neighbours.First());
        }

        [Fact]
        public void NeighboursInNineCellWorld()
        {
            // arrange
            var worldSize = new WorldSize(3, 3);
            var location = new Location(1, 1 );

            // set expectation
            var excpectedNeighboursCount = 8;

            // act
            var neighbours = location.GetNeighbours(worldSize);

            var allNeighboursRowAreOneStepFarFromOriginal = neighbours.All(l => l.Row >= 0 && l.Row <= 2);
            var allNeighboursColumnsAreOneStepFarFromOriginal = neighbours.All(l => l.Column >= 0 && l.Column <= 2);

            // assert
            Assert.NotNull(neighbours);
            Assert.Equal(excpectedNeighboursCount, neighbours.Count);
            Assert.True(allNeighboursRowAreOneStepFarFromOriginal);
            Assert.True(allNeighboursColumnsAreOneStepFarFromOriginal);
            Assert.True(neighbours.All(l => l != location));
        }

    }


}
