using Amazon.Infrastructure.General;
using Amazon.Infrastructure.General.EventAggregator;
using Amazon.Infrastructure.General.Interfaces;
using Amazon.Infrastructure.General.Interfaces.EventAggregator;
using Amazon.OrderFulfillment.WPF.ViewModel.Orders;
using Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM.Commands;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace Amazon.OrderFulfillment.WPF.IOCContainer
{
    /// <summary>
    /// All the utility/infrastructure dependencies should be registered here
    /// </summary>
    public class UtilitiesDependenciesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITimer>().To<Timer>().WithConstructorArgument("frequencyInMilliseconds", (long)2000);
            Bind<IOrdersStateChangeObserver>().To<OrdersStateChangeObserver>().InTransientScope();
            Bind<IEventManager>().To<EventManager>().InSingletonScope();
            Bind<IOrderFactory>().ToFactory().InTransientScope();
        }
    }
}