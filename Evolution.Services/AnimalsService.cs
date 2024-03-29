﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;
using Evolution.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Services
{
    public class AnimalsService : IAnimalsService
    {
        private IEvolutionContext Context { get; }
        private IAnimalsFactory AnimalsFactory { get; }
        private IGameCalender GameCalender { get; }

        public AnimalsService(
            IEvolutionContext context,
            IAnimalsFactory animalsFactory,
            IGameCalender gameCalender)
        {
            Context = context;
            AnimalsFactory = animalsFactory;
            GameCalender = gameCalender;


            Console.WriteLine("new AnimalsService");
        }

        public async Task Act(Guid id)
        {
            var animal = await LoadAnimal(id);
            if (animal == null) return;

            animal.Act(GameCalender.Now);

            await Context.SaveChangesAsync();
        }

        public async Task<string> Act()
        {
            var nextAnimal = Context.Animals
                .Where(a => a.IsAlive)
                .OrderBy(a => a.NextAction)
                .FirstOrDefault(a => a.NextAction <= GameCalender.Now);

            if (nextAnimal == null) return null;

            await Act(nextAnimal.Id);
            return nextAnimal.Name;
        }

        public async Task CreateNew(string name)
        {
            var settings = Context.GameSettings.First();
            var newAnimal = AnimalsFactory.CreateNew(name, settings);
            await Context.Animals.AddAsync(newAnimal);
            await Context.SaveChangesAsync();
        }

        public async Task Kill(Guid id)
        {
            var animal = await Context.Animals.FindAsync(id);
            animal.Die();
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAll()
        {
            Context.Animals.RemoveRange(Context.Animals);
            await Context.SaveChangesAsync();
        }

        public async Task<IList<AnimalDto>> Get(DateTime after)
        {
            var animals = await Context
                .Animals
                .Where(a => a.LastAction >= after)
                .ToListAsync();

            //var newLast = animals.Max(a => a.LastAction);
            //var count = animals.Count();

            //Console.WriteLine($"after:{after} | new:{newLast} | Count:{count}");

            return animals.Select(MapToDto).ToList();
        }

        public async Task<AnimalDto> Get(Guid id)
        {
            var animal = await Context.Animals.FindAsync(id);
            return MapToDto(animal);
        }

        private AnimalDto MapToDto(Animal animal)
        {
            return new AnimalDto()
            {
                Id = animal.Id,
                IsAlive = animal.IsAlive,
                Weight = animal.Weight,
                ChildrenCount = animal.ChildrenCount,
                CreationTime = animal.CreationTime,
                DeathTime = animal.DeathTime,
                MinEnergy = animal.MinEnergy,
                MaxEnergy = animal.MaxEnergy,
                Energy = animal.Energy,
                MinFoodStorageCapacity = animal.MinFoodStorageCapacity,
                MaxFoodStorageCapacity = animal.MaxFoodStorageCapacity,
                FoodStorageCapacity = animal.FoodStorageCapacity,
                Name = animal.Name,
                ParentId = animal.ParentId,
                MinSpeed = animal.MinSpeed,
                MaxSpeed = animal.MaxSpeed,
                Speed = animal.Speed,
                Steps = animal.Steps,
                StoredFood = animal.StoredFood,
                LastAction = animal.LastAction,
                NextAction = animal.NextAction,
                LastChildAt = animal.LastChildAt,
                MinSense = animal.MinSense,
                MaxSense = animal.MaxSense,
                Sense = animal.Sense,
                Age = animal.GetAge(GameCalender.Now),
                Direction = animal.Direction.ToString(),
                StepCost = animal.StepCost,
                Location = new LocationDto()
                {
                    Column = animal.Location.Column,
                    Row = animal.Location.Row,
                    Name = animal.Location.Name
                }
            };
        }

        private async Task<Animal> LoadAnimal(Guid id)
        {
            var animal = await Context
                .Animals
                .Include(a => a.Settings)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null) return null;

            var rowMinFilter = animal.Location.Row - animal.Sense;
            var rowMaxFilter = animal.Location.Row + animal.Sense;
            var colMinFilter = animal.Location.Column - animal.Sense;
            var colMaxFilter = animal.Location.Column + animal.Sense;
            var food = await Context.Plants
                .Where(p => p.IsAlive
                            && p.Weight > 0
                            && p.Location.Row >= rowMinFilter
                            && p.Location.Row <= rowMaxFilter
                            && p.Location.Column >= colMinFilter
                            && p.Location.Column <= colMaxFilter)
                .ToListAsync();

            // TODO: temp solution for a bug
            if (food.Any(f => f.Weight <= 0))
            {
                food = food.Where(f => f.Weight > 0).ToList();
            }

            AnimalsFactory.Initialize(animal, food);

            return animal;
        }

    }
}