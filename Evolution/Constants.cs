namespace Evolution
{
    public static class Constants
    {
        // one game hour = 60 sec = 1 min 
        public const int GameHourToRealSecondRatio = 60;

        // assuming world is square
        // world will start at (WorldEdgeStart, WorldEdgeStart) or (0, 0) and end at (WorldEdgeEnd, WorldEdgeEnd)
        public const int WorldEdgeStart = 0;
        public const int WorldEdgeEnd = 12;
    }
}