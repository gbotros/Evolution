using System;
using System.Threading.Tasks;
using Evolution.Blueprints;

namespace Evolution.Abstractions
{
    public interface IPlantService
    {
        Task<EatIntoOperationResult> EatInto(Guid plantId, int neededAmount);
        Task<bool> Update(PlantBlueprint plant);
    }
}