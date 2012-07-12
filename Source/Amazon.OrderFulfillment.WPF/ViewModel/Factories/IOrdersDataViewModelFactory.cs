using System.Collections.Generic;
using Amazon.OrderFulfillment.WPF.ViewModel.Orders;

namespace Amazon.OrderFulfillment.WPF.ViewModel.Factories
{
    public interface IOrdersDataViewModelFactory
    {
        IOrdersDataViewModel Create(IEnumerable<dynamic> orderValues);
    }
}