using System;
using System.Threading.Tasks;

namespace Evolution.Services
{
    public interface IPlantsService
    {
        Task Act(Guid plantId);
        Task CreateNew(Guid? parentId);
    }
}