using Evolution.Blueprints;

namespace Evolution.Abstractions
{
    public interface IAnimal : ICreature
    {
        AnimalBlueprint Blueprint { get; }
        int Speed { get; }
        int Fight(int neededAmount);
    }
}