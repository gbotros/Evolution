
namespace Evolution.Domain.Common
{
    public interface IFood
    {
        Location Location { get; }
        int Weight { get; }
        int EatInto(int desiredAmount);
    }
}