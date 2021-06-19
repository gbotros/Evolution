using Evolution.Domain.GameSettingsAggregate;

namespace Evolution.Domain.Common
{
    public interface ILocationService
    {
        Location GetRandom(WorldSize worldSize);
    }
}