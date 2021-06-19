using System;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;

namespace Evolution.Domain.GameSettingsAggregate
{
    public class GameSettings : AggregateRoot
    {
        protected GameSettings()
        {

        }

        public GameSettings(Guid id, WorldSize worldSize, AnimalDefaults animalDefaults)
        {
            Id = id;
            WorldSize = worldSize;
            AnimalDefaults = animalDefaults;
        }

        public WorldSize WorldSize { get; private set; }
        public AnimalDefaults AnimalDefaults { get; private set; }
    }
}