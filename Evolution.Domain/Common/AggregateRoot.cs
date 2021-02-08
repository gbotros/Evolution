using System;
using System.Collections.Generic;
using Evolution.Domain.Events;

namespace Evolution.Domain.Common
{
    public abstract class AggregateRoot : Entity
    {

        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

        protected AggregateRoot()
        {

        }

        public AggregateRoot(Guid id) : base(id)
        {

        }

        protected void RaiseEvent(IDomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
        }

        protected void ClearEvents()
        {
            domainEvents.Clear();
        }


    }
}
