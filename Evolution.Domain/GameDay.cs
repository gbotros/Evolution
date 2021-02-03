using System;
using Evolution.Domain.Common;

namespace Evolution.Domain
{
    public class GameDays : ValueObject<GameDays>
    {
        private const int RealSecondInGameHour = 60; // 60 real sec = 1 game hour

        public double Value { get; }

        public GameDays(double value)
        {
            Value = value;
        }

        public GameDays(TimeSpan ageTimeSpan)
        {
            Value = ConvertToAge(ageTimeSpan);
        }

        private double ConvertToAge(TimeSpan ageTimeSpan)
        {
            var ageInSecs = ageTimeSpan.TotalSeconds;
            var differenceInGameHour = ageInSecs / RealSecondInGameHour;
            var differenceInGameDays = differenceInGameHour / 24;
            return differenceInGameDays;
        }

        protected override bool EqualsCore(GameDays other)
        {
            return Value == other.Value;
        }

        protected override int GetHashCodeCore()
        {
            return Value.GetHashCode();
        }

        public static bool operator >(GameDays a, GameDays b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(GameDays a, GameDays b)
        {
            return a.Value < b.Value;
        }
        public static bool operator >=(GameDays a, GameDays b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(GameDays a, GameDays b)
        {
            return a.Value <= b.Value;
        }
    }
}