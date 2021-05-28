using System;
using Evolution.Domain.Common;
using Xunit;

namespace Evolution.Domain.Tests.LocationTests
{
    public class ConstructorTests
    {

        [Fact]
        public void LocationMustBeWithinWorldBoundaries()
        {
            // negative coordinates
            Assert.Throws<ApplicationException>(() => new Location(-1, 0, 1, 1));
            Assert.Throws<ApplicationException>(() => new Location(0, -1, 1, 1));
            Assert.Throws<ApplicationException>(() => new Location(-1, -1, 1, 1));

            // very small world
            Assert.Throws<ApplicationException>(() => new Location(0, 0, 0, 1));
            Assert.Throws<ApplicationException>(() => new Location(0, 0, 1, 0));
            Assert.Throws<ApplicationException>(() => new Location(0, 0, 0, 0));
        }

    }

}
