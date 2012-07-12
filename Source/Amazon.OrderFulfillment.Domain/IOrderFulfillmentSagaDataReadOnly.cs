using System;

namespace Amazon.OrderFulfillment.Domain
{
    public interface IOrderFulfillmentSagaDataReadOnly
    {
        string Id { get; }

        IOrder Order { get; }

        ConfigState OrderStarted { get; }

        ConfigState PaymentProcessed { get; }

        ConfigState FulfillmentStarted { get; }

        TimeSpan DurationOfFulfillment { get; }

        ConfigState Shipped { get; }

        ConfigState Delivered { get; }

        ConfigState ProductIsReturned { get; }

        ConfigState CustomerIsRefunded { get; }

        ConfigState Cancelled { get; }

        ConfigState Complete { get; }

        ConfigState SagaComplete { get; }
    }
}