using Amazon.OrderFulfillment.WPF.ViewModel.Factories;
using Amazon.OrderFulfillment.WPF.ViewModel.Orders;
using Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM;
using Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM.Commands;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace Amazon.OrderFulfillment.WPF.IOCContainer
{
    /// <summary>
    /// All the view models should be registered here
    /// </summary>
    public class ViewModelDependenciesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrdersFulfillmentViewModel>().To<OrdersFulfillmentViewModel>().InTransientScope();                
            Bind<IOrdersViewModel>().To<OrdersViewModel>().InTransientScope();
            Bind<IOrdersDataViewModel>().To<OrdersDataViewModel>().InTransientScope();
            Bind<IOrdersDataViewModelFactory>().ToFactory();
            Bind<IStartOrderCommand>().To<StartOrderCommand>().InSingletonScope();
        }
    }
}