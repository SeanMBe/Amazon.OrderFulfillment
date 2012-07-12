using System.Collections.Generic;
using System.Linq;

namespace Amazon.OrderFulfillment.WPF.ViewModel.Orders
{
    public class OrdersDataViewModel : IOrdersDataViewModel
    {
        public OrdersDataViewModel(IEnumerable<object> orderValues)
        {
            Values = orderValues.ToArray();
        }

        public object[] Values { get; private set; }
    }
}