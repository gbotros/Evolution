using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Services
{
    public interface IPlantsService
    {
        Task<IList<PlantDto>> GetAllAlive();
        //Task<PlantDto> Get(Guid plantId);
        //Task GrowAll();
        Task AddFoodAtRandomPlaces(int count);
        //Task Act(Guid plantId);
        //Task CreateNew();
        Task DeleteAll();
    }
}