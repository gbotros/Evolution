using System.Threading.Tasks;
using Evolution.Blueprints;
using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface ILocationFactory
    {
        Task<ILocation> Create(LocationBlueprint blueprint);
    }
}