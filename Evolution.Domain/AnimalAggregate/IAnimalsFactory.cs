using System;
using Evolution.Domain.Common;

namespace Evolution.Domain.AnimalAggregate
{
    public interface IAnimalsFactory
    {
        Animal CreateNew(string name);

        Animal CreateNew(string name, Location location, int energy, int foodStorageCapacity, int speed, Guid? parentId);
    }
}