using System;
using System.Collections.Concurrent;
using Evolution.Domain.Events;

namespace Evolution.Domain.Common
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {


        private readonly ConcurrentQueue<IDomainEvent> domainEvents = new();
        public IProducerConsumerCollection<IDomainEvent> DomainEvents => domainEvents;

        protected AggregateRoot()
        {

        }

        protected AggregateRoot(Guid id) : base(id)
        {

        }

        protected void RaiseEvent(IDomainEvent domainEvent)
        {
            domainEvents.Enqueue(domainEvent);
        }
        
    }
}
