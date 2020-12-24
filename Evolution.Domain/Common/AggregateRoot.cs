//using System.Collections.Generic;

//namespace Evolution.Domain.Common
//{
//    public class AggregateRoot : Entity
//    {
//        private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();
//        public virtual IReadOnlyList<IDomainEvent> DomainEvents => domainEvents;

//        protected virtual void AddDomainEvent(IDomainEvent newEvent)
//        {
//            domainEvents.Add(newEvent);
//        }

//        public virtual void ClearEvents()
//        {
//            domainEvents.Clear();
//        }
//    }
//}
