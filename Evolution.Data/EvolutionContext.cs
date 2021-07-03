using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;
using Evolution.Domain.GameSettingsAggregate;
using Evolution.Domain.PlantAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evolution.Data
{
    public class EvolutionContext : DbContext, IEvolutionContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<GameSettings> GameSettings { get; set; }

        private EvolutionContextOptions Options { get; }

        private IMediator Mediator { get; }

        public EvolutionContext(
            EvolutionContextOptions options,
            IMediator mediator)
        {
            Options = options;
            Mediator = mediator;

            Console.WriteLine("new EvolutionContext");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await DispatchDomainEvents();
            var res = await base.SaveChangesAsync(cancellationToken);
            return res;
        }

        public async Task GrowAll()
        {
            await Database.ExecuteSqlRawAsync("UPDATE Plants SET Weight = Weight + GrowthAmount WHERE IsAlive = 1");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) =>
                        category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
                    .AddConsole();
            });

            optionsBuilder
                .UseSqlServer(Options.ConnectionString);

            if (Options.UseConsoleLogger)
            {
                optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapAnimals(modelBuilder);
            MapPlants(modelBuilder);
            MapGameSettings(modelBuilder);
        }

        private static void MapAnimals(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>(
                animalEntityBuilder =>
                {
                    animalEntityBuilder.OwnsOne(animal => animal.Location, navigationBuilder =>
                    {
                        navigationBuilder.Property(l => l.Row).HasColumnName("Row");
                        navigationBuilder.Property(l => l.Column).HasColumnName("Column");
                    });
                    
                    animalEntityBuilder.Ignore(animal => animal.Food);
                }

            );
        }

        private static void MapPlants(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant>(plantEntityBuilder =>
            {
                plantEntityBuilder.OwnsOne(plant => plant.Location, navigationBuilder =>
                {
                    navigationBuilder.Property(l => l.Row).HasColumnName("Row");
                    navigationBuilder.Property(l => l.Column).HasColumnName("Column");
                });
                
            });
        }

        private void MapGameSettings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameSettings>(

                settingsEntityBuilder =>
                {
                    settingsEntityBuilder.OwnsOne(gs => gs.WorldSize);
                    settingsEntityBuilder.OwnsOne(gs => gs.AnimalDefaults);
                }

            );
        }

        private async Task DispatchDomainEvents()
        {
            var roots = ChangeTracker.Entries<IAggregateRoot>()
                .Select(po => po.Entity)
                .Where(po => po.DomainEvents.Any())
                .ToArray();

            foreach (var r in roots)
            {
                while (r.DomainEvents.TryTake(out var dv))
                {
                    await Mediator.Publish(dv);  //Dispatcher.Dispatch(dv);
                }
            }
        }
    }
}

