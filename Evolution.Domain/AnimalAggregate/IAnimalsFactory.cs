using System;
using System.Collections.Generic;
using Evolution.Domain.Common;
using Evolution.Domain.GameSettingsAggregate;

namespace Evolution.Domain.AnimalAggregate
{
    public interface IAnimalsFactory
    {
        Animal CreateNew(string name, GameSettings settings);
        void Initialize(Animal animal, IReadOnlyCollection<IPlantFood> food);

        Animal CreateNew(
            string name,
            Guid? parentId,
            Location location,
            GameSettings settings,
            int energy,
            int foodStorageCapacity,
            int speed,
            int oneFoodToEnergy,
            int adulthoodAge,
            int minSpeed,
            int maxSpeed,
            uint speedMutationAmplitude,
            int minEnergy,
            int maxEnergy);

    }
}