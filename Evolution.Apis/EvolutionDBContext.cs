using Evolution.Apis.Models;
using Evolution.Blueprints;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Apis
{
    public class EvolutionDbContext : DbContext
    {
        public EvolutionDbContext()
        {
        }

        public EvolutionDbContext(DbContextOptions<EvolutionDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AnimalBlueprint> Animals { get; set; }
        public virtual DbSet<PlantBlueprint> Plants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalBlueprint>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.OwnsOne(typeof(LocationBlueprint), "Location");
            });

            modelBuilder.Entity<PlantBlueprint>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.OwnsOne(typeof(LocationBlueprint), "Location");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
        }
    }
}