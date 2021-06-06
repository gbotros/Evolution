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
        private EvolutionContext Context { get; }
        private WorldSize WorldSize { get; }
        private IAnimalsFactory AnimalsFactory { get; }
        private ILocationService LocationService { get; }
        private IGameCalender GameCalender { get; }

        public AnimalsService(
            EvolutionContext context,
            WorldSize worldSize,
            IAnimalsFactory animalsFactory, 
            ILocationService locationService,
            IGameCalender gameCalender)
        {
            Context = context;
            WorldSize = worldSize;
            AnimalsFactory = animalsFactory;
            LocationService = locationService;
            GameCalender = gameCalender;
        }

        public async Task Act(Guid id)
        {
            var animal = await Context.Animals.FindAsync(id);
            if (animal == null) return;
            var food = Context.Plants.Where(p =>
                p.IsAlive
                && p.Location.Row == animal.Location.Row
                && p.Location.Column == animal.Location.Column).ToList();
            AnimalsFactory.Initialize(animal, food);

            animal.Act(GameCalender.Now, WorldSize);

            await Context.SaveChangesAsync();
        }

        public async Task CreateNew(string name)
        {
            var newAnimal = AnimalsFactory.CreateNew(name);
            await Context.Animals.AddAsync(newAnimal);
            await Context.SaveChangesAsync();
        }

        public async Task Kill(Guid id)
        {
            var animal = await Context.Animals.FindAsync(id);
            animal.Die();
            await Context.SaveChangesAsync();
        }

        public async Task<IList<AnimalDto>> Get()
        {
             var animals = await Context.Animals.ToListAsync();
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
                Energy = animal.Energy,
                FoodStorageCapacity = animal.FoodStorageCapacity,
                Name = animal.Name,
                ParentId = animal.ParentId,
                Speed = animal.Speed,
                Steps = animal.Steps,
                StoredFood = animal.StoredFood,
                LastAction = animal.LastAction,
                NextAction = animal.NextAction,
                Location = new LocationDto()
                {
                    Column = animal.Location.Column,
                    Row = animal.Location.Row,
                    Name = animal.Location.Name
                }
            };
        }
    }
}