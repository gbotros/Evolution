using System;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Blueprints;

namespace Evolution.Services.Http
{
    public class PlantService : IPlantService
    {
        public Task<EatIntoOperationResult> EatInto(Guid plantId, int neededAmount)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(PlantBlueprint plant)
        {
            throw new NotImplementedException();
        }
    }
}