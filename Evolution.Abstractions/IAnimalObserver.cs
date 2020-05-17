using System.Threading.Tasks;
using Evolution.Blueprints;

namespace Evolution.Abstractions
{
    public interface IAnimalObserver
    {
        Task<bool> OnEat(AnimalBlueprint animal, ICreature food);
        Task<bool> OnMove(AnimalBlueprint animal);
        Task<bool> OnReproduce(AnimalBlueprint parent, AnimalBlueprint son);
    }
}