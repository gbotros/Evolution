namespace Evolution.Domain
{
    public interface INeighbourLocation
    {
        int Food { get; }
        string Name { get; }
        int X { get; }
        int Y { get; }
        bool IsEmpty();
    }
}