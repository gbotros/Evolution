using System;

namespace Evolution.Abstractions
{
    public interface IPlantService
    {
        int EatInto(Guid plantId, int neededAmount);
    }
}