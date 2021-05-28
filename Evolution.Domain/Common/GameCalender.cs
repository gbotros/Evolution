using System;

namespace Evolution.Domain.Common
{
    public class GameCalender : IGameCalender
    {
        public DateTime Now => DateTime.UtcNow;
    }
}