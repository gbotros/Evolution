using Evolution.Blueprints;
using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface ILocationService
    {
        ILocation GetLocation(LocationBlueprint blueprint);
    }
}