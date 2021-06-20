using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Web.Services
{
    public interface IAnimalsService
    {
        Task CreateNew(string newAnimalName);
        Task Kill(Guid id);
        Task Act(Guid id);
        Task<List<AnimalDto>> GetAll(DateTime after);
    }
}