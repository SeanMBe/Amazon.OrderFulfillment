using System;
using System.Collections.Generic;

namespace Amazon.OrderFulfillment.Domain
{
    public interface IOrderFulfillmentSagaManager
    {
        event Action<object, EventArgs> SagasChanged;
        
        IEnumerable<IOrderFulfillmentSaga> Sagas { get; }
    }
}