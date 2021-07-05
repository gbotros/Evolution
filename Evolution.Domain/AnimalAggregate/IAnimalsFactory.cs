using System;
using System.Collections.Generic;
using Evolution.Domain.Common;
using Evolution.Domain.GameSettingsAggregate;

namespace Evolution.Domain.AnimalAggregate
{
    public interface IAnimalsFactory
    {
        Animal CreateNew(string name, GameSettings settings);
        void Initialize(Animal animal, IReadOnlyCollection<IFood> food);

        Animal CreateNew(
            string name,
            Guid? parentId,
            Location location,
            GameSettings settings,
            double energy,
            int minFoodStorageCapacity,
            int maxFoodStorageCapacity,
            int foodStorageCapacity,
            double speed,
            int oneFoodToEnergy,
            int adulthoodAge,
            double minSpeed,
            double maxSpeed,
            uint speedMutationAmplitude,
            double minEnergy,
            double maxEnergy,
            int sense,
            int minSense,
            int maxSense,
            uint senseMutationAmplitude);

    }
}