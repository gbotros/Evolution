namespace Evolution.Abstractions
{
    public interface IAnimalObserver
    {
        bool OnEat(IAnimal animal, ICreature food);
        bool OnMove(IAnimal animal);
        bool OnReproduce(IAnimal parent, IAnimal son);
    }
}