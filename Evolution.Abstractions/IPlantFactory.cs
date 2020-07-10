using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface IPlantFactory
    {
        IPlant Create(PlantBlueprint plantBlueprint);
    }
}