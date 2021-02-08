
namespace Evolution.Domain.Common
{
    public interface IPlantFood
    {
        int Weight { get; }
        int EatInto(int desiredAmount);
    }
}