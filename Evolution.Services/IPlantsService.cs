using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Services
{
    public interface IPlantsService
    {
        Task<IList<PlantDto>> Get();
        Task<PlantDto> Get(Guid plantId);
        Task GrowAll();
        Task Act(Guid plantId);
        Task CreateNew();
        Task DeleteAll();
    }
}