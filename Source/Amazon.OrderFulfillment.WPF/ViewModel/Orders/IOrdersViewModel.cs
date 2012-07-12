using System.Collections.Generic;

namespace Amazon.OrderFulfillment.WPF.ViewModel.Orders
{
    public interface IOrdersViewModel
    {
        IEnumerable<IOrdersDataViewModel> Orders { get; }
    }
}