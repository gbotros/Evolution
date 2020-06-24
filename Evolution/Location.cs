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
        private const string IntFormat = "D2";

        public Location(LocationBlueprint blueprint,
            IEnumerable<AnimalBlueprint> animals = null,
            IEnumerable<PlantBlueprint> plants = null,
            IEnumerable<LocationBlueprint> neighbours = null)
        {
            X = blueprint.X;
            Y = blueprint.Y;
            Blueprint = blueprint;
            Animals = animals;
            Plants = plants;
            Neighbours = neighbours;
        }

        public IEnumerable<AnimalBlueprint> Animals { get; set; }

        public LocationBlueprint Blueprint { get; }

        public Guid Id { get; set; }

        public string Name => X.ToString(IntFormat) + "," + Y.ToString(IntFormat);

        public IEnumerable<LocationBlueprint> Neighbours { get; set; }

        public IEnumerable<PlantBlueprint> Plants { get; set; }

        public int X { get; }

        public int Y { get; }

        public bool IsEmpty()
        {
            return !Animals.Any() && !Plants.Any();
        }
    }
}