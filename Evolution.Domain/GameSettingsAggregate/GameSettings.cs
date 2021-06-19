using System;
using Evolution.Domain.Common;

namespace Evolution.Domain.GameSettingsAggregate
{
    public class GameSettings : AggregateRoot
    {
        protected GameSettings()
        {

        }

        public GameSettings(Guid id, WorldSize worldSize = null, AnimalDefaults animalDefaults = null)
        {
            Id = id;
            WorldSize = worldSize ?? new WorldSize();
            AnimalDefaults = animalDefaults ?? new AnimalDefaults();
        }

        public WorldSize WorldSize { get; set; }
        public AnimalDefaults AnimalDefaults { get; set; }
    }
}