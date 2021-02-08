using System;

namespace Evolution.Domain
{
    public class GameCalender : IGameCalender
    {
        public DateTime Now => DateTime.UtcNow;
    }
}