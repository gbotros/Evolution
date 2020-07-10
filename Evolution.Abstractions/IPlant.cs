using System.Threading.Tasks;
using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface IPlant : ICreature
    {
        PlantBlueprint GetBlueprint();
        Task Act();
    }
}