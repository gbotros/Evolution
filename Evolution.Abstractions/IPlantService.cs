using System;
using System.Threading.Tasks;
using Evolution.Blueprints;
using Evolution.Dtos;

namespace Evolution.Abstractions
{
    public interface IPlantService
    {
        Task<EatIntoResult> EatInto(Guid plantId, int neededAmount);
        Task<int> Update(PlantBlueprint plant);
    }
}