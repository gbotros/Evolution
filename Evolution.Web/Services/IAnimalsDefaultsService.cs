using System.Threading.Tasks;
using Evolution.Dtos;

namespace Evolution.Web.Services
{
    public interface IAnimalsDefaultsService
    {
        Task Reset(AnimalDefaultsDto animalDefaultsDto);
    }
}