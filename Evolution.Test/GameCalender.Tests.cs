using System;
using Xunit;

namespace Evolution.Test
{
    public class LocationTests
    {
        [Theory]
        [InlineData(1, 0, 0, 1, 0, 0, 0)]
        [InlineData(1, 0, 0, 1, 0, 24, 1)]
        [InlineData(1, 0, 0, 1, 0, 36, 1.5)]
        [InlineData(1, 0, 0, 1, 0, 48, 2)]
        [InlineData(1, 0, 0, 1, 1, 0, 2.5)]
        [InlineData(1, 0, 0, 2, 0, 0, 60)]
        public void CalculateDifferenceInGameDays_GameRatio_60_To_1(int fromDay, int fromHour, int fromMin, int toDay,
            int toHour, int toMin, double expected)
        {
            // arrange
            var gameHourToRealSecondRatio = 60; // real time to game time ratio 60:1
            var calender = new GameCalender(gameHourToRealSecondRatio);
            var from = new DateTime(2020, 1, fromDay, fromHour, fromMin, 0, DateTimeKind.Utc);
            var to = new DateTime(2020, 1, toDay, toHour, toMin, 0, DateTimeKind.Utc);

            // act
            var actual = calender.CalculateDifferenceInGameDays(from, to);

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0, 1, 0, 0)]
        [InlineData(1, 0, 1, 12, 0.5)]
        [InlineData(1, 0, 2, 0, 1)]
        public void CalculateDifferenceInGameDays_Ratio_1_To_1(int fromDay, int fromHour, int toDay, int toHour,
            double expected)
        {
            // arrange
            var gameHourToRealSecondRatio = 60 * 60; // real time to game time ratio 1:1
            var calender = new GameCalender(gameHourToRealSecondRatio);
            var from = new DateTime(2020, 1, fromDay, fromHour, 0, 0, DateTimeKind.Utc);
            var to = new DateTime(2020, 1, toDay, toHour, 0, 0, DateTimeKind.Utc);

            // act
            var actual = calender.CalculateDifferenceInGameDays(from, to);

            // assert
            Assert.Equal(expected, actual);
        }
    }
}