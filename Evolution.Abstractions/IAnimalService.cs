using System.Threading.Tasks;
using Evolution.Blueprints;
using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface IAnimalService
    {
        Task<bool> Add(AnimalBlueprint animal);
        Task<bool> Update(AnimalBlueprint animal);
    }
}