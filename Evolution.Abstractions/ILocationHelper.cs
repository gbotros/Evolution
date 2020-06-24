using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface ILocationHelper
    {
        LocationBlueprint GetEastLocation(LocationBlueprint location);
        LocationBlueprint GetNorthEastLocation(LocationBlueprint location);
        LocationBlueprint GetNorthLocation(LocationBlueprint location);
        LocationBlueprint GetNorthWestLocation(LocationBlueprint location);
        LocationBlueprint GetSouthEastLocation(LocationBlueprint location);
        LocationBlueprint GetSouthLocation(LocationBlueprint location);
        LocationBlueprint GetSouthWestLocation(LocationBlueprint location);
        LocationBlueprint GetWestLocation(LocationBlueprint location);
    }
}