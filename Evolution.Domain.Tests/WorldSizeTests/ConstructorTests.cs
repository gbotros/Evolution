using System;
using Evolution.Domain.Common;
using FluentAssertions;
using Xunit;

namespace Evolution.Domain.Tests.WorldSizeTests
{
    public class ConstructorTests
    {

        [Fact]
        public void WorldMustBeAtLeastOneCell()
        {
            Assert.Throws<ApplicationException>(() => new WorldSize(0, 1));
            Assert.Throws<ApplicationException>(() => new WorldSize(1, 0));
            Assert.Throws<ApplicationException>(() => new WorldSize(0, 0));
        }

    }

}
