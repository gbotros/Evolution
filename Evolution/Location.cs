using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Abstractions;
using Evolution.Blueprints;
using Evolution.Entities;

namespace Evolution
{
    public class Location : ILocation
    {
        public Location(LocationBlueprint blueprint,
            ILocationNameHelper nameHelper,
            IEnumerable<ICreature> creatures = null,
            IEnumerable<ILocation> neighbours = null)
        {
            X = blueprint.X;
            Y = blueprint.Y;
            Blueprint = blueprint;
            NameHelper = nameHelper;
            Creatures = creatures;
            Neighbours = neighbours;
        }

        public IEnumerable<IAnimal> Animals => Creatures.OfType<IAnimal>();

        public LocationBlueprint Blueprint { get; }
        public IEnumerable<ICreature> Creatures { get; }

        public Guid Id { get; set; }

        public string Name => NameHelper.GetLocationName(X, Y);

        public ILocationNameHelper NameHelper { get; }

        public IEnumerable<ILocation> Neighbours { get; set; }

        public IEnumerable<IPlant> Plants => Creatures.OfType<IPlant>();

        public int X { get; }

        public int Y { get; }

        public bool IsEmpty()
        {
            return Creatures != null && !Creatures.Any();
        }
    }
}