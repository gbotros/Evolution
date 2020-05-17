using Evolution.Blueprints;

namespace Evolution.Abstractions
{
    public interface ILocationService
    {
        ILocation GetLocation(LocationBlueprint blueprint);
    }
}