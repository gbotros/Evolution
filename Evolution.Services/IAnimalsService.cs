using System;
using System.Threading.Tasks;
using Evolution.Domain.AnimalAggregate;

namespace Evolution.Services
{
    public interface IAnimalsService
    {
        Task<Animal> Get(Guid id);
        Task Act(Guid id);
        Task CreateNew(string name);
    }
}