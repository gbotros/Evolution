using System;

namespace Evolution.Domain
{
    public class GameCalender : IGameCalender
    {
        public GameCalender(int gameHourToRealSecondRatio)
        {
            GameHourToRealSecondRatio = gameHourToRealSecondRatio;
        }

        private int GameHourToRealSecondRatio { get; }

        public double CalculateDifferenceInGameDays(DateTime fromDate, DateTime toDate)
        {
            var difference = toDate - fromDate;
            var differenceInSec = difference.TotalSeconds;
            var differenceInGameHour = differenceInSec / GameHourToRealSecondRatio;
            var differenceInGameDays = differenceInGameHour / 24;
            return differenceInGameDays;
        }
    }
}