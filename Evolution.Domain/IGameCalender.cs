using System;

namespace Evolution.Domain
{
    public interface IGameCalender
    {
        double CalculateDifferenceInGameDays(DateTime fromDate, DateTime toDate);
    }
}