using System.Threading;
using System.Threading.Tasks;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.GameSettingsAggregate;
using Evolution.Domain.PlantAggregate;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Data
{
    public interface IEvolutionContext
    {
        DbSet<Animal> Animals { get; set; }
        DbSet<Plant> Plants { get; set; }
        DbSet<GameSettings> GameSettings { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task GrowAll();
    }
}