namespace Evolution.Blueprints
{
    public class LocationBlueprint
    {
        public LocationBlueprint()
        {
        }

        public LocationBlueprint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}