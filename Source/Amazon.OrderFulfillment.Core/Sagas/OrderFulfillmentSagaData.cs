using System;
using Amazon.OrderFulfillment.Domain;

namespace Amazon.OrderFulfillment.Core.Sagas
{
    public class OrderFulfillmentSagaData : IOrderFulfillmentSagaDataReadWrite
    {

        public OrderFulfillmentSagaData(IOrder order)
        {
            Order = order;
            OrderStarted = ConfigState.Good;
            PaymentProcessed = ConfigState.Unknown;
            FulfillmentStarted = ConfigState.Unknown;
            Shipped= ConfigState.Unknown;
            Delivered= ConfigState.Unknown;
            ProductIsReturned= ConfigState.Unknown;
            CustomerIsRefunded= ConfigState.Unknown;
            Cancelled= ConfigState.Unknown;
            Complete= ConfigState.Unknown;
        }

        public string Id
        {
            get { return Order.Id; }
        }

        public IOrder Order { get; private set; }
        public ConfigState OrderStarted { get; private set; }
        public ConfigState PaymentProcessed { get; private set; }
        public ConfigState FulfillmentStarted { get; private set; }
        public TimeSpan DurationOfFulfillment { get { return TimeSpan.FromSeconds(0); } }
        public ConfigState Shipped { get; private set; }
        public ConfigState Delivered { get; private set; }
        public ConfigState ProductIsReturned { get; private set; }
        public ConfigState CustomerIsRefunded { get; private set; }
        public ConfigState Cancelled { get; private set; }
        public ConfigState Complete { get; private set; }

        public ConfigState SagaComplete { get; private set; }

        public void SetOrderStartedGood()
        {
            OrderStarted = ConfigState.Good;
        }

        public void SetSagaPending()
        {
            Complete = ConfigState.Pending;
        }

        public void SetSagaComplete()
        {
            Complete = ConfigState.Good;
        }
    }
}