using System;

namespace Evolution.Abstractions
{
    public interface IGameCalender
    {
        double CalculateDifferenceInGameDays(DateTime fromDate, DateTime toDate);
    }
}