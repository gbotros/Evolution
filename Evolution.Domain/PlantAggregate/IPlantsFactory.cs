using System;

namespace Evolution.Domain.PlantAggregate
{
    public interface IPlantsFactory
    {
        Plant CreateNew(Guid? parentId);
    }
}