using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.PlantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Evolution.Data
{
    public class EvolutionContext : DbContext
    {
        private string ConnectionString { get; }
        private bool UseConsoleLogger { get; }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Plant> Plants { get; set; }

        public EvolutionContext(string connectionString, bool useConsoleLogger)
        {
            ConnectionString = connectionString;
            UseConsoleLogger = useConsoleLogger;
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
                .UseSqlServer(ConnectionString);

            if (UseConsoleLogger)
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
                    navigationBuilder.Property(location => location.Row).HasColumnName("Row");
                    navigationBuilder.Property(location => location.Column).HasColumnName("Column");
                });
            });
        }

    }
}

