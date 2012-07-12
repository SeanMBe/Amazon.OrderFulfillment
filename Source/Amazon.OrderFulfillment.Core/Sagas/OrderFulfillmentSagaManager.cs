using System;
using System.Collections.Generic;
using Amazon.Infrastructure.General.Interfaces.EventAggregator;
using Amazon.OrderFulfillment.Domain;
using MavenThought.Commons.Extensions;

namespace Amazon.OrderFulfillment.Core.Sagas
{
    public class OrderFulfillmentSagaManager : IOrderFulfillmentSagaManager
    {
        readonly IEventManager _eventManager;
        readonly ISagaFactory _sagaFactory;
        readonly IDictionary<string, IOrderFulfillmentSaga> _sagas;
        readonly object _handleSagaLock;

        public OrderFulfillmentSagaManager(IEventManager eventManager, ISagaFactory sagaFactory)
        {
            _eventManager = eventManager;
            _sagaFactory = sagaFactory;
            _sagas = new Dictionary<string, IOrderFulfillmentSaga>();
            _handleSagaLock = new object();
            
            SagasChanged = delegate { };

            SubscribeToEvents();
        }

        public event Action<object, EventArgs> SagasChanged;

        public IEnumerable<IOrderFulfillmentSaga> Sagas
        {
            get
            {
                lock (_handleSagaLock)
                {
                    return _sagas.Values;
                }
            }
        }

        void SubscribeToEvents()
        {
           _eventManager.Subscribe<IEventOrderStarted>(e => HandleSagaEvent(typeof(IEventOrderStarted), e));
        }

        void HandleSagaEvent(Type eventType, dynamic theEvent)
        {
            lock (_handleSagaLock)
            {
                var saga = GetSaga(theEvent.Order);

                //invoke with reflection to avoid duplication for each Handle of saga
                var method = saga.GetType().GetMethod("Handle", new Type[] {eventType});
                method.Invoke(saga, new object[] {theEvent});

                SagasChanged(this, new EventArgs());
            }
        }
      
        IOrderFulfillmentSaga GetSaga(IOrder order)
        {
            if (!_sagas.Keys.Exists(k => k == order.Id))
            {
                _sagas.Add(order.Id, _sagaFactory.Create(order));
            }

            var saga = _sagas[order.Id];
            return saga;
        }
    }
}