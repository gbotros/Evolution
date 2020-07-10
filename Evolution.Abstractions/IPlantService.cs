using System.Collections.Generic;
using System.Threading.Tasks;
using Evolution.Abstractions.Dtos;
using Evolution.Entities;

namespace Evolution.Abstractions
{
    public interface IPlantService
    {
        Task<IEnumerable<PlantBlueprint>> GetAll();
        Task<IEnumerable<PlantBlueprint>> GetByLocation(LocationBlueprint location);
        Task<UpdatePlantResponseDto> Update(UpdatePlantDto dto);
    }
}