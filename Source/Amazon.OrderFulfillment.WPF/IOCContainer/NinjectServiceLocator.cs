using System;
using System.Collections.Generic;
using MavenThought.Commons.Extensions;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Ninject.Modules;

namespace Amazon.OrderFulfillment.WPF.IOCContainer
{
    public class NinjectServiceLocator : ServiceLocatorImplBase
    {
        private IEnumerable<INinjectModule> _ninjectModules;
        private readonly StandardKernel _ninjectKernel;

        public NinjectServiceLocator()
        {
            _ninjectKernel = new StandardKernel();
            RegisterModules();
            LoadKernel();
        }

        private void RegisterModules()
        {
            _ninjectModules = Enumerable.Create<INinjectModule>(
                new CordDomainModule(),
                new UtilitiesDependenciesModule(),
                new ViewModelDependenciesModule(),
                new SagaDependenciesModule(),
                new GenericInfrastructureDependenciesModule());
        }

        private void LoadKernel()
        {
            _ninjectKernel.Load(_ninjectModules);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _ninjectKernel.Get(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _ninjectKernel.GetAll(serviceType);
        }
    }
}
