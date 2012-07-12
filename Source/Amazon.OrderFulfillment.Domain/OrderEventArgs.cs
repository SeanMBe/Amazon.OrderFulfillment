using System;

namespace Amazon.OrderFulfillment.Domain
{
    public class OrderEventArgs : EventArgs
    {
        public OrderEventArgs(IOrder order)
        {
            Order = order;
        }

        public IOrder Order { get; private set; }
    }
}