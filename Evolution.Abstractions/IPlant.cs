using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface IPlant : ICreature
    {
        PlantBlueprint Blueprint { get; }
    }
}