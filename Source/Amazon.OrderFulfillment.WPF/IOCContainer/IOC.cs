using Microsoft.Practices.ServiceLocation;

namespace Amazon.OrderFulfillment.WPF.IOCContainer
{
    public static class IOC
    {
        public static void SetLocator(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
        }

        public static IServiceLocator ServiceLocator { get; private set; }
    }
}
