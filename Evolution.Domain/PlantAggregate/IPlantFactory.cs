using System;

namespace Evolution.Domain.PlantAggregate
{
    public interface IPlantFactory
    {
        Plant CreateNew(Guid? parentId);
    }
}