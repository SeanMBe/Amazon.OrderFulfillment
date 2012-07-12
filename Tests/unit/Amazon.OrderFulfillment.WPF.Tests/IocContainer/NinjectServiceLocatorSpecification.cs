using System;
using System.Collections.Generic;
using Amazon.Infrastructure.General.Interfaces;
using Amazon.Infrastructure.General.Interfaces.EventAggregator;
using Amazon.OrderFulfillment.Core.Order;
using Amazon.OrderFulfillment.Domain;
using Amazon.OrderFulfillment.WPF.IOCContainer;
using Amazon.OrderFulfillment.WPF.ViewModel.Factories;
using Amazon.OrderFulfillment.WPF.ViewModel.Orders;
using Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM;
using Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM.Commands;
using Machine.Specifications;
using MavenThought.Commons.Extensions;
using developwithpassion.specifications.rhinomocks;

namespace Amazon.OrderFulfillment.WPF.Unit.Tests.IocContainer
{
    public class NinjectServiceLocatorSpecification : Observes<NinjectServiceLocator>
    {
        private Establish c = () =>
        {
            typesToResolveAsTransient = Enumerable.Create(
                typeof(ITimer),
                typeof(IOrder),
                typeof(IOrdersStateChangeObserver),
                typeof(IOrdersFulfillmentViewModel),
                typeof(IOrdersDataViewModelFactory),
                typeof(IOrdersFulfillmentViewModel),
                typeof(IOrderFulfillmentSagaDataFactory),
                typeof(ISagaFactory)
                );

            typesToResolveAsSingleton = Enumerable.Create(
                typeof(IEventManager),
                typeof(IOrderFulfillmentSagaManager),
                typeof(ILog),
                typeof(IStartOrderCommand)
                );

            factoriesShouldWork = Enumerable.Create(
                //IOrderFactory
                new Tuple<Type, Func<object, object>, Predicate<object>>(
                    typeof(IOrderFactory), //factory to test
                    f => ((IOrderFactory)f).Create(), //whate method of factory to test
                    v => v != null), //the expected result

                //IDeviceConfiStatusViewModelFactory
                new Tuple<Type, Func<object, object>, Predicate<object>>(
                    typeof(IOrdersDataViewModelFactory), //factory to test
                    f => ((IOrdersDataViewModelFactory)f).Create(new List<object>() { 1, 2, 3 }),
                //what method of factory to test
                    v => //the expected result
                    {
                        var values =
                            System.Linq.Enumerable.ToArray(
                                System.Linq.Enumerable.Cast<int>(((IOrdersDataViewModel)v).Values));
                        return values.Length == 3 && values[0] == 1 && values[1] == 2 && values[2] == 3;
                    }),



            //ISagaFactory => Create a telephony factory config saga
            new Tuple<Type, Func<object, object>, Predicate<object>>(
            typeof(ISagaFactory), //factory to test
            f =>
            {
                helper = new Order();
                return ((ISagaFactory)f).Create(helper);
            }, //what method of factory to test
            v => ((IOrderFulfillmentSaga)v).SagaId == ((Order)helper).Id), //the expected result

            //ISagaDataFactory => Create a telephony factory config saga data
            new Tuple<Type, Func<object, object>, Predicate<object>>(
            typeof(IOrderFulfillmentSagaDataFactory), //factory to test
            f =>
            {
                helper = new Order();
                return ((IOrderFulfillmentSagaDataFactory)f).Create(helper);
            }, //what method of factory to test
            v => ((IOrderFulfillmentSagaDataReadOnly)v).Id == ((Order)helper).Id)); //the expected result

        };

        public static IEnumerable<Type> typesToResolveAsTransient;
        public static IEnumerable<Type> typesToResolveAsSingleton;
        public static IEnumerable<Tuple<Type, Func<object, object>,Predicate<object>>> factoriesShouldWork;
        static dynamic helper;
    }

    public class When_ninject_service_locator_has_expected_types_resolved : NinjectServiceLocatorSpecification
    {
        It should_be_able_to_resolve_all_the_types_as_transient =
            () => typesToResolveAsTransient.ForEach(t =>
                                                        {
                                                            var firstInstance = sut.GetInstance(t);
                                                            var secondInstance = sut.GetInstance(t);
                                                            firstInstance.ShouldNotBeNull();
                                                            secondInstance.ShouldNotBeNull();
                                                            firstInstance.ShouldNotBeTheSameAs(secondInstance);
                                                        });

        It should_be_able_to_resolve_all_the_singletons_as_singletons =
            () => typesToResolveAsSingleton.ForEach(t =>
                                             {
                                                 var firstInstance = sut.GetInstance(t);
                                                 var secondInstance = sut.GetInstance(t);
                                                 firstInstance.ShouldBeTheSameAs(secondInstance);
                                                 firstInstance.ShouldNotBeNull();
                                             });

        It should_have_all_factories_working =
            () => factoriesShouldWork.ForEach(item1IsTheFactoryTypeAndItem2ExcerciseTheFactoryAndItem3AssertsTheCreationIsCorrect =>
            {
                var factory = sut.GetInstance(item1IsTheFactoryTypeAndItem2ExcerciseTheFactoryAndItem3AssertsTheCreationIsCorrect.Item1);
                var result = item1IsTheFactoryTypeAndItem2ExcerciseTheFactoryAndItem3AssertsTheCreationIsCorrect.Item2(factory);
                result.ShouldNotBeNull();
                item1IsTheFactoryTypeAndItem2ExcerciseTheFactoryAndItem3AssertsTheCreationIsCorrect.Item3(result).ShouldEqual(true);
            });

    }
}


