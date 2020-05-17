using Evolution.Blueprints;

namespace Evolution.Abstractions
{
    public interface IPlant : ICreature
    {
        PlantBlueprint Blueprint { get; }
    }
}