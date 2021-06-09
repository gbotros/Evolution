using System.Threading;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Events;
using MediatR;

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
            var newAnimal = AnimalsFactory.CreateNew(
                notification.Name,
                notification.Location,
                notification.Energy,
                notification.FoodStorageCapacity,
                notification.Speed,
                notification.ParentId);

            await Context.Animals.AddAsync(newAnimal, cancellationToken);
        }
    }


}
