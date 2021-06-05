using Evolution.Domain.Common;
using Xunit;

namespace Evolution.Domain.Tests.LocationTests
{
    public class NameTests
    {
        [Fact]
        public void NameCell_0_0()
        {

            // arrange
            var location = new Location(0, 0);

            // set expectation
            var expectedName = "Cell (0000, 0000)";

            // act
            var actualName = location.Name;

            // assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void NameCell_99_99()
        {

            // arrange
            var location = new Location(99, 99);

            // set expectation
            var expectedName = "Cell (0099, 0099)";

            // act
            var actualName = location.Name;

            // assert
            Assert.Equal(expectedName, actualName);
        }
        
        [Fact]
        public void NameCell_100_100()
        {

            // arrange
            var location = new Location(100, 100);

            // set expectation
            var expectedName = "Cell (0100, 0100)";

            // act
            var actualName = location.Name;

            // assert
            Assert.Equal(expectedName, actualName);
        }

    }

}
