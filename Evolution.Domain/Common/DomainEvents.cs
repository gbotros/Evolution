//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//namespace Evolution.Domain.Common
//{
//    public static class DomainEvents
//    {
//        private static List<Type> handlers;

//        public static void Init()
//        {
//            handlers = Assembly.GetExecutingAssembly()
//                .GetTypes()
//                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>)))
//                .ToList();
//        }

//        public static void Dispatch(IDomainEvent domainEvent)
//        {
//            foreach (Type handlerType in handlers)
//            {
//                bool canHandleEvent = handlerType.GetInterfaces()
//                    .Any(x => x.IsGenericType
//                        && x.GetGenericTypeDefinition() == typeof(IHandler<>)
//                        && x.GenericTypeArguments[0] == domainEvent.GetType());

//                if (canHandleEvent)
//                {
//                    dynamic handler = Activator.CreateInstance(handlerType);
//                    handler.Handle((dynamic)domainEvent);
//                }
//            }
//        }
//    }
//}
