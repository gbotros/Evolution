using System.Threading;
using System.Threading.Tasks;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.PlantAggregate;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Data
{
    public interface IEvolutionContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Plant> Plants { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}