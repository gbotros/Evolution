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
            IEnumerable<AnimalBlueprint> animals = null,
            IEnumerable<PlantBlueprint> plants = null,
            IEnumerable<ILocation> neighbours = null)
        {
            X = blueprint.X;
            Y = blueprint.Y;
            Blueprint = blueprint;
            NameHelper = nameHelper;
            Animals = animals;
            Plants = plants;
            Neighbours = neighbours;
        }

        public IEnumerable<AnimalBlueprint> Animals { get; set; }

        public LocationBlueprint Blueprint { get; }

        public Guid Id { get; set; }

        public string Name => NameHelper.GetLocationName(X, Y);

        public ILocationNameHelper NameHelper { get; }

        public IEnumerable<ILocation> Neighbours { get; set; }

        public IEnumerable<PlantBlueprint> Plants { get; set; }

        public int X { get; }

        public int Y { get; }

        public bool IsEmpty()
        {
            return !Animals.Any() && !Plants.Any();
        }
    }
}