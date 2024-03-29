﻿using System;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.GameSettingsAggregate;
using Evolution.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Services
{
    public class GameSettingsService : IGameSettingsService
    {
        private IEvolutionContext Context { get; }
        public GameSettingsService(IEvolutionContext context)
        {
            Context = context;
        }

        public async Task UpdateOrInsert(GameSettingsDto dto)
        {
            var orgSettings = await Context.GameSettings.FirstOrDefaultAsync();
            var settings = orgSettings ?? new GameSettings(Guid.NewGuid());

            settings.WorldSize = new WorldSize(dto.WorldSize.Width, dto.WorldSize.Height);
            settings.AnimalDefaults = new AnimalDefaults()
            {
                Speed = dto.AnimalDefaults.Speed,
                Energy = dto.AnimalDefaults.Energy,
                MinFoodStorageCapacity = dto.AnimalDefaults.MinFoodStorageCapacity,
                MaxFoodStorageCapacity = dto.AnimalDefaults.MaxFoodStorageCapacity,
                FoodStorageCapacity = dto.AnimalDefaults.FoodStorageCapacity,
                AdulthoodAge = dto.AnimalDefaults.AdulthoodAge,
                MaxEnergy = dto.AnimalDefaults.MaxEnergy,
                MaxSpeed = dto.AnimalDefaults.MaxSpeed,
                MinEnergy = dto.AnimalDefaults.MinEnergy,
                MinSpeed = dto.AnimalDefaults.MinSpeed,
                OneFoodToEnergy = dto.AnimalDefaults.OneFoodToEnergy,
                SpeedMutationAmplitude = dto.AnimalDefaults.SpeedMutationAmplitude,
                Sense = dto.AnimalDefaults.Sense,
                MinSense = dto.AnimalDefaults.MinSense,
                MaxSense = dto.AnimalDefaults.MaxSense,
                SenseMutationAmplitude = dto.AnimalDefaults.SenseMutationAmplitude,
            };

            if (orgSettings == null)
            {
                await Context.GameSettings.AddAsync(settings);
            }
            await Context.SaveChangesAsync();
        }

        public async Task<GameSettingsDto> Get()
        {
            GameSettingsDto dto;
            var settings = await Context.GameSettings.FirstOrDefaultAsync();
            if (settings != null)
            {
                dto = new GameSettingsDto()
                {
                    WorldSize = new WorldSizeDto()
                    {
                        Height = settings.WorldSize.Height,
                        Width = settings.WorldSize.Width
                    },
                    AnimalDefaults = new AnimalDefaultsDto()
                    {
                        Speed = settings.AnimalDefaults.Speed,
                        Energy = settings.AnimalDefaults.Energy,
                        MinFoodStorageCapacity = settings.AnimalDefaults.MinFoodStorageCapacity,
                        MaxFoodStorageCapacity = settings.AnimalDefaults.MaxFoodStorageCapacity,
                        FoodStorageCapacity = settings.AnimalDefaults.FoodStorageCapacity,
                        OneFoodToEnergy = settings.AnimalDefaults.OneFoodToEnergy,
                        AdulthoodAge = settings.AnimalDefaults.AdulthoodAge,
                        MaxEnergy = settings.AnimalDefaults.MaxEnergy,
                        MinEnergy = settings.AnimalDefaults.MinEnergy,
                        MaxSpeed = settings.AnimalDefaults.MaxSpeed,
                        MinSpeed = settings.AnimalDefaults.MinSpeed,
                        SpeedMutationAmplitude = settings.AnimalDefaults.SpeedMutationAmplitude,
                        Sense = settings.AnimalDefaults.Sense,
                        MinSense = settings.AnimalDefaults.MinSense,
                        MaxSense = settings.AnimalDefaults.MaxSense,
                        SenseMutationAmplitude = settings.AnimalDefaults.SenseMutationAmplitude
                    }
                };
            }
            else
            {
                dto = new GameSettingsDto()
                {
                    AnimalDefaults = new AnimalDefaultsDto(),
                    WorldSize = new WorldSizeDto()
                };
            }

            return dto;
        }
    }
}
