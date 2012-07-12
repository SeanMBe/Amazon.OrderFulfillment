using Amazon.Infrastructure.General.Interfaces.EventAggregator;
using Amazon.OrderFulfillment.Domain;

namespace Amazon.OrderFulfillment.Core.Sagas
{
    public class OrderFulfillmentSaga : IOrderFulfillmentSaga
    {
        readonly IEventManager _eventManager;
        readonly IOrderFulfillmentSagaDataReadWrite _sagaData;

        public OrderFulfillmentSaga(IOrder order, IOrderFulfillmentSagaDataFactory sagaDataFactory, IEventManager eventManager)
        {
            _eventManager = eventManager;
            SagaId = order.Id;
            _sagaData = sagaDataFactory.Create(order);
        }

        public void Handle(IEventOrderStarted theEvent)
        {
            _sagaData.SetOrderStartedGood();
        }

        public string SagaId { get; private set; }

        public IOrderFulfillmentSagaDataReadOnly SagaData 
        {
            get { return _sagaData; }
        }

        bool CheckIfComplete()
        {
            return _sagaData.SagaComplete == ConfigState.Good;
        }
    }
}
