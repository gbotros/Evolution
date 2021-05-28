using Evolution.Domain.Common;
using Xunit;

namespace Evolution.Domain.Tests.LocationTests
{
    public class EqualTests
    {

        Location location1 = new Location(0, 0, 2, 2);
        Location location2 = new Location(0, 0, 5, 5);
        Location location3 = new Location(1, 0, 5, 5);

        [Fact]
        public void EqualOperator_True()
        {
            Assert.True(location1 == location2);
            Assert.False(location1 == location3);
        }

        [Fact]
        public void NotEqualOperator_False()
        {
            Assert.False(location1 != location2);
            Assert.True(location1 != location3);
        }

    }

}
