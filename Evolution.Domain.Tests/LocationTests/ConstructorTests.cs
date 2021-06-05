using System;
using Evolution.Domain.Common;
using FluentAssertions;
using Xunit;

namespace Evolution.Domain.Tests.LocationTests
{
    public class ConstructorTests
    {

        [Fact]
        public void LocationMustBeWithinWorldBoundaries()
        {
            var oneCellWorld = new WorldSize(1, 1);

            // negative coordinates
            var l1 = new Location(-1, 0);
            var l2 = new Location(0, -1);
            var l3 = new Location(-1, -1);

            l1.IsValid(oneCellWorld).Should().BeFalse();
            l2.IsValid(oneCellWorld).Should().BeFalse();
            l3.IsValid(oneCellWorld).Should().BeFalse();
            
        }

    }

}
