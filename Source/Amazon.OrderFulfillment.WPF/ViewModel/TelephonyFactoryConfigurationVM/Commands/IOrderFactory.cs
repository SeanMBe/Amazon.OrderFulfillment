using Amazon.OrderFulfillment.Domain;

namespace Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM.Commands
{
    public interface IOrderFactory
    {
        IOrder Create();
    }
}
