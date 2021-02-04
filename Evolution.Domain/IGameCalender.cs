using System;

namespace Evolution.Domain
{
    public interface IGameCalender
    {
        DateTime Now { get; }
    }
}