using Amazon.Infrastructure.General;
using Amazon.Infrastructure.General.Interfaces;
using Amazon.OrderFulfillment.WPF.IOCContainer;

namespace Amazon.OrderFulfillment.WPF
{
    public static class BootStrapper
    {
        public static void Begin()
        {
            IOC.SetLocator(new NinjectServiceLocator());
            Logger.SetLogger(IOC.ServiceLocator.GetInstance<ILog>());
        }
    }
}