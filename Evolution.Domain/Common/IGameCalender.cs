using System;

namespace Evolution.Domain.Common
{
    public interface IGameCalender
    {
        DateTime Now { get; }
    }
}