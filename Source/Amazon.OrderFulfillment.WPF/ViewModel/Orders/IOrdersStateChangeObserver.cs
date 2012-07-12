using System;
using System.Collections.Generic;

namespace Amazon.OrderFulfillment.WPF.ViewModel.Orders
{
    public interface IOrdersStateChangeObserver
    {
        IEnumerable<IEnumerable<object>> OrdersData { get; }

        event Action<object, EventArgs> OrdersStateChanged;
    }
}