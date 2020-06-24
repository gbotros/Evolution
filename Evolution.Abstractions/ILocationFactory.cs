using System.Threading.Tasks;
using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface ILocationFactory
    {
        Task<ILocation> Create(LocationBlueprint blueprint);
        Task<ILocation> CreateEmpty(LocationBlueprint blueprint);
    }
}