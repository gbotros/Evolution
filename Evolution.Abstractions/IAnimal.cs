namespace Evolution.Abstractions
{
    public interface IAnimal : ICreature
    {
        int Speed { get; }
        int Fight(int neededAmount);
    }
}