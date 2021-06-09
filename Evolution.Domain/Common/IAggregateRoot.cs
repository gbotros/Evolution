using System.Collections.Concurrent;
using Evolution.Domain.Events;

namespace Evolution.Domain.Common
{
    public interface IAggregateRoot
    {
        IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
    }
}