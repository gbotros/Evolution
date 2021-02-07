using Xunit;

namespace Evolution.Domain.LocationTests
{
    public class NameTests
    {
        [Fact]
        public void NameCell_0_0_OneCellWorld()
        {

            // arrange
            var worldWidth = 1;
            var worldHeight = 1;
            var location = new Location(0, 0, worldWidth, worldHeight);

            // set expectation
            var expectedName = "Cell (0, 0)";

            // act
            var actualName = location.Name;

            // assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void NameCell_99_99_OneHundredCellWorld()
        {

            // arrange
            var worldWidth = 100;
            var worldHeight = 100;
            var location = new Location(99, 99, worldWidth, worldHeight);

            // set expectation
            var expectedName = "Cell (99, 99)";

            // act
            var actualName = location.Name;

            // assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void NameCell_99_99_OneThousandCellWorld()
        {

            // arrange
            var worldWidth = 1000;
            var worldHeight = 1000;
            var location = new Location(99, 99, worldWidth, worldHeight);

            // set expectation
            var expectedName = "Cell (099, 099)";

            // act
            var actualName = location.Name;

            // assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void NameCell_100_100()
        {

            // arrange
            var worldWidth = 101;
            var worldHeight = 101;
            var location = new Location(100, 100, worldWidth, worldHeight);

            // set expectation
            var expectedName = "Cell (100, 100)";

            // act
            var actualName = location.Name;

            // assert
            Assert.Equal(expectedName, actualName);
        }

    }

}
