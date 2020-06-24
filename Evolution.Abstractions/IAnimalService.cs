using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface IAnimalService
    {
        Task<bool> Add(AnimalBlueprint animal);
        Task<IEnumerable<AnimalBlueprint>> GetByLocation(LocationBlueprint location);
        Task<bool> Update(AnimalBlueprint animal);
    }
}