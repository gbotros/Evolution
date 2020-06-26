using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface IPlantService
    {
        Task<EatIntoOperationResult> EatInto(Guid plantId, int neededAmount);
        Task<IEnumerable<PlantBlueprint>> GetByLocation(LocationBlueprint location);
        Task<bool> Update(PlantBlueprint plant);
    }
}