using System;
using System.Collections.Generic;
using Evolution.Domain.Common;

namespace Evolution.Domain.AnimalAggregate
{
    public interface IAnimalsFactory
    {
        Animal CreateNew(string name);
        void Initialize(Animal animal, IReadOnlyCollection<IPlantFood> food);

        Animal CreateNew(string name, Location location, int energy, int foodStorageCapacity, int speed, Guid? parentId);
    }
}