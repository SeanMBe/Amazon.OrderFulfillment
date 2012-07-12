using Amazon.Infrastructure.General;
using Amazon.Infrastructure.General.Interfaces;
using Ninject.Modules;

namespace Amazon.OrderFulfillment.WPF.IOCContainer
{
    /// <summary>
    /// All the classes to be used in generic infrastructure should be registered here
    /// </summary>
    public class GenericInfrastructureDependenciesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().To<Log>().InSingletonScope();
        }
    }
}