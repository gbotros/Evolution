using System;
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

        public virtual DbSet<Creatures> Creatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Creatures>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Speed)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }
    }
}