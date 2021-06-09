using System;

namespace Evolution.Domain.Common
{
    public class GameDays : ValueObject<GameDays>
    {
        private const int RealSecondInGameHour = 60; // 60 real sec = 1 game hour
        private const byte HoursPerDay = 24;

        public double Value { get; }
        public TimeSpan TimeSpan { get; }

        public GameDays(double value)
        {
            Value = value;
            TimeSpan = ConvertToTimeSpan(value);
        }

        public GameDays(TimeSpan ageTimeSpan)
        {
            Value = ConvertToAge(ageTimeSpan);
            TimeSpan = ageTimeSpan;
        }

        public double ToGameHours()
        {
            return Value / HoursPerDay;
        }

        // TODO: unit tests
        private double ConvertToAge(TimeSpan ageTimeSpan)
        {
            var ageInSecs = ageTimeSpan.TotalSeconds;
            var differenceInGameHour = ageInSecs / RealSecondInGameHour;
            var differenceInGameDays = differenceInGameHour / HoursPerDay;
            return differenceInGameDays;
        }

        // TODO: unit tests
        private TimeSpan ConvertToTimeSpan(double days)
        {
            var gameHours = days * HoursPerDay;
            var realSeconds = gameHours * RealSecondInGameHour;
            var realSecondsInt = Convert.ToInt32(realSeconds);
            var timeSpan = new TimeSpan(0, 0, realSecondsInt);
            return timeSpan;
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

        public static GameDays FromGameHours(double hours)
        {
            var days = hours / HoursPerDay;
            return new GameDays(days);
        }
    }
}