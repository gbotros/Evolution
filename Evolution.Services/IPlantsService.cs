using System;
using System.Threading.Tasks;
using Evolution.Domain.PlantAggregate;

namespace Evolution.Services
{
    public interface IPlantsService
    {
        Task<Plant> Get(Guid plantId);
        Task Act(Guid plantId);
        Task CreateNew(Guid? parentId);
    }
}