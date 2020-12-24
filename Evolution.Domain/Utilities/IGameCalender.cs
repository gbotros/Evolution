using System;

namespace Evolution.Domain.Utilities
{
    public interface IGameCalender
    {
        double CalculateDifferenceInGameDays(DateTime fromDate, DateTime toDate);
    }
}