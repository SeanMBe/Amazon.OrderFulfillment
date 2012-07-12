using System.Windows.Input;
using Amazon.OrderFulfillment.WPF.ViewModel.Orders;

namespace Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM
{
    public interface IOrdersFulfillmentViewModel 
    {
        IOrdersDataViewModel Orders { get; }
        ICommand StartOrder { get; }
        bool StartOrderEnabled { get; }
    }
}