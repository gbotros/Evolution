using Evolution.Blueprints;

namespace Evolution.Abstractions
{
    public interface IAnimal : ICreature
    {
        int Speed { get; }
        AnimalBlueprint Blueprint { get; }
        int Fight(int neededAmount);
    }
}