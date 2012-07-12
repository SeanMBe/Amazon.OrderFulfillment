using Amazon.OrderFulfillment.Core.Sagas;
using Amazon.OrderFulfillment.Domain;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace Amazon.OrderFulfillment.WPF.IOCContainer
{
    /// <summary>
    /// All the saga related dependencies should be registered here
    /// </summary>
    public class SagaDependenciesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISagaFactory>().ToFactory().InTransientScope();
            Bind<IOrderFulfillmentSaga>().To<OrderFulfillmentSaga>().InTransientScope();
            Bind<IOrderFulfillmentSagaDataFactory>().ToFactory().InTransientScope();
            Bind<IOrderFulfillmentSagaDataReadWrite>().To<OrderFulfillmentSagaData>();
            Bind<IOrderFulfillmentSagaManager>().To<OrderFulfillmentSagaManager>().InSingletonScope();
        }
    }
}