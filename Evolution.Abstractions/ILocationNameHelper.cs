namespace Evolution.Abstractions
{
    public interface ILocationNameHelper
    {
        string GetEastLocationName(int x, int y);
        string GetLocationName(int x, int y);
        string GetNorthEastLocationName(int x, int y);
        string GetNorthLocationName(int x, int y);
        string GetNorthWestLocationName(int x, int y);
        string GetSouthEastLocationName(int x, int y);
        string GetSouthLocationName(int x, int y);
        string GetSouthWestLocationName(int x, int y);
        string GetWestLocationName(int x, int y);
    }
}