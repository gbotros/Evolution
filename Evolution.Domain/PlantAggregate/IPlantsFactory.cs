using System;
using Evolution.Domain.GameSettingsAggregate;

namespace Evolution.Domain.PlantAggregate
{
    public interface IPlantsFactory
    {
        Plant CreateNew(GameSettings settings, Guid? parentId = null);
    }
}