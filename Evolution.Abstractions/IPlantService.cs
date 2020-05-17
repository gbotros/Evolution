using System;

namespace Evolution.Abstractions
{
    public interface IFoodService
    {
        int EatInto(Guid plantId, int neededAmount);
    }
}