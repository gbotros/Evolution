using System.Threading.Tasks;

namespace Evolution.Domain
{
    public interface IPlant : ICreature
    {
        PlantBlueprint GetBlueprint();
        void Act();
    }
}