using System;
using System.Threading.Tasks;
using Evolution.Abstractions;
using Evolution.Blueprints;
using Evolution.Dtos;

namespace Evolution.Services.Http
{
    public class PlantService : IPlantService
    {
        public Task<EatIntoResponse> EatInto(Guid plantId, int neededAmount)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(PlantBlueprint plant)
        {
            throw new NotImplementedException();
        }
    }
}