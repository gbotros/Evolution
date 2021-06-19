using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;
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
            var size = new WorldSize(dto.WorldSize.Width, dto.WorldSize.Height);
            var defaults = new AnimalDefaults()
            {
                Speed = dto.AnimalDefaults.Speed,
                Energy = dto.AnimalDefaults.Energy,
                FoodStorageCapacity = dto.AnimalDefaults.FoodStorageCapacity,
                AdulthoodAge = dto.AnimalDefaults.AdulthoodAge,
                MaxEnergy = dto.AnimalDefaults.MaxEnergy,
                MaxSpeed = dto.AnimalDefaults.MaxSpeed,
                MinEnergy = dto.AnimalDefaults.MinEnergy,
                MinSpeed = dto.AnimalDefaults.MinSpeed,
                OneFoodToEnergy = dto.AnimalDefaults.OneFoodToEnergy,
                SpeedMutationAmplitude = dto.AnimalDefaults.SpeedMutationAmplitude
            };

            var orgSettings = await Context.GameSettings.FirstOrDefaultAsync();
            var exist = orgSettings != null;
            var id = exist ? orgSettings.Id : Guid.NewGuid();
            var settings = new GameSettings(id, size, defaults);

            if (exist)
            {
                Context.GameSettings.Attach(settings);
            }
            else
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
                        FoodStorageCapacity = settings.AnimalDefaults.FoodStorageCapacity,
                        OneFoodToEnergy = settings.AnimalDefaults.OneFoodToEnergy,
                        AdulthoodAge = settings.AnimalDefaults.AdulthoodAge,
                        MaxEnergy = settings.AnimalDefaults.MaxEnergy,
                        MinEnergy = settings.AnimalDefaults.MinEnergy,
                        MaxSpeed = settings.AnimalDefaults.MaxSpeed,
                        MinSpeed = settings.AnimalDefaults.MinSpeed,
                        SpeedMutationAmplitude = settings.AnimalDefaults.SpeedMutationAmplitude
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
