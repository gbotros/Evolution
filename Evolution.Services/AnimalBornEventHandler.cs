using System.Threading;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Evolution.Services
{
    public class AnimalBornEventHandler : INotificationHandler<AnimalBornEvent>
    {
        private IEvolutionContext Context { get; }
        private IAnimalsFactory AnimalsFactory { get; }

        public AnimalBornEventHandler(IEvolutionContext context, IAnimalsFactory animalsFactory)
        {
            Context = context;
            AnimalsFactory = animalsFactory;
        }

        public async Task Handle(AnimalBornEvent notification, CancellationToken cancellationToken)
        {
            var settings = await Context.GameSettings.FirstAsync(cancellationToken);
            var newAnimal = AnimalsFactory.CreateNew(
                notification.Name,
                notification.ParentId,
                notification.Location,
                settings,
                notification.Energy,
                notification.FoodStorageCapacity,
                notification.Speed,
                notification.OneFoodToEnergy,
                notification.AdulthoodAge,
                notification.MinSpeed,
                notification.MaxSpeed,
                notification.SpeedMutationAmplitude,
                notification.MinEnergy,
                notification.MaxEnergy,
                notification.Sense
            );

            await Context.Animals.AddAsync(newAnimal, cancellationToken);
        }
    }


}
