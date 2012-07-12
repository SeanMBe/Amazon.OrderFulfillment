using Amazon.OrderFulfillment.Core.Order;
using Amazon.OrderFulfillment.Domain;
using Ninject.Modules;

namespace Amazon.OrderFulfillment.WPF.IOCContainer
{
    /// <summary>
    /// All the network dependencies should be registered here
    /// </summary>
    public class CordDomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrder>().To<Order>().InTransientScope();
        }
    }
}