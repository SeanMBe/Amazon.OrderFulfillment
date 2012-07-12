using System;
using Amazon.OrderFulfillment.Domain;

namespace Amazon.OrderFulfillment.Core.Order
{
    public class Order : IOrder
    {
        public Order()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; private set; }
    }
}
