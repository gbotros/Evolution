using System;
using System.Threading.Tasks;
using Evolution.Data;
using Evolution.Domain.AnimalAggregate;
using Evolution.Domain.Common;

namespace Evolution.Services
{
    public class AnimalsService : IAnimalsService
    {
        private EvolutionContext Context { get; }
        private IAnimalsFactory AnimalsFactory { get; }
        private ILocationService LocationService { get; }
        private IGameCalender GameCalender { get; }

        public AnimalsService(EvolutionContext context, IAnimalsFactory animalsFactory, ILocationService locationService, IGameCalender gameCalender)
        {
            Context = context;
            AnimalsFactory = animalsFactory;
            LocationService = locationService;
            GameCalender = gameCalender;
        }

        public async Task Act(Guid id)
        {
            var animal = await Context.Animals.FindAsync(id);
            if (animal == null) return;

            animal.Act(GameCalender.Now);

            await Context.SaveChangesAsync();
        }

        public async Task CreateNew(string name)
        {
            var newAnimal = AnimalsFactory.CreateNew(name);
            await Context.Animals.AddAsync(newAnimal);
            await Context.SaveChangesAsync();
        }

        public async Task<Animal> Get(Guid id) => await Context.Animals.FindAsync(id);
    }
}