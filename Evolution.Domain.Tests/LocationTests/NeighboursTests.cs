using System.Linq;
using Xunit;

namespace Evolution.Domain.LocationTests
{
    public class NeighboursTests
    {

        [Fact]
        public void NeighboursInOneCellWorld()
        {
            // arrange
            var worldWidth = 1;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);

            // set expectation
            var excpectedNeighboursCount = 0;

            // act
            var neighbours = location.Neighbours;

            // assert
            Assert.NotNull(neighbours);
            Assert.Equal(excpectedNeighboursCount, neighbours.Count);
        }

        [Fact]
        public void NeighboursInTwoCellWorld()
        {
            // arrange
            var worldWidth = 2;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);
            var location01 = new Location(0, 1, worldWidth, worldHeight);

            // set expectation
            var excpectedNeighboursCount = 1;

            // act
            var neighbours = location.Neighbours;

            // assert
            Assert.NotNull(neighbours);
            Assert.Equal(excpectedNeighboursCount, neighbours.Count);
            Assert.Equal(location01, neighbours.First());
        }

        [Fact]
        public void NeighboursInNineCellWorld()
        {
            // arrange
            var worldWidth = 3;
            var worldHeight = 3;
            var location = new Location(1, 1, worldWidth, worldHeight);

            // set expectation
            var excpectedNeighboursCount = 8;

            // act
            var neighbours = location.Neighbours;

            var allNeighboursRowAreOneStepFarFromOriginal = neighbours.All(l => l.Row >= 0 && l.Row <= 2);
            var allNeighboursColumnsareOneStepFarFromOriginal = neighbours.All(l => l.Column >= 0 && l.Column <= 2);

            // assert
            Assert.NotNull(neighbours);
            Assert.Equal(excpectedNeighboursCount, neighbours.Count);
            Assert.True(allNeighboursRowAreOneStepFarFromOriginal);
            Assert.True(allNeighboursColumnsareOneStepFarFromOriginal);
            Assert.True(neighbours.All(l => l != location));
        }

    }


}
