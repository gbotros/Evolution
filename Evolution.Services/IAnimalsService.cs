using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Services
{
    public interface IAnimalsService
    {
        Task<IList<AnimalDto>> Get();
        Task<AnimalDto> Get(Guid id);
        Task Act(Guid id);
        Task<bool> Act();
        Task CreateNew(string name);
        Task Kill(Guid id);
        Task DeleteAll( );
    }
}