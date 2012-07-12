using System.Collections.Generic;
using Amazon.OrderFulfillment.WPF.Common;
using Amazon.OrderFulfillment.WPF.ViewModel.Factories;
using MavenThought.Commons.Extensions;

namespace Amazon.OrderFulfillment.WPF.ViewModel.Orders
{
    public class OrdersViewModel : BaseNotifyPropertyChanged, IOrdersViewModel
    {
        readonly IOrdersStateChangeObserver _ordersStateChangeObserver;
        readonly IOrdersDataViewModelFactory _ordersDataViewModelFactory;

        public OrdersViewModel(IOrdersStateChangeObserver ordersStateChangeObserver, IOrdersDataViewModelFactory ordersDataViewModelFactory)
        {
            _ordersStateChangeObserver = ordersStateChangeObserver;
            _ordersDataViewModelFactory = ordersDataViewModelFactory;
            HandleDeviceConfigurationChanges();
        }

        public void HandleDeviceConfigurationChanges()
        {
            _ordersStateChangeObserver.OrdersStateChanged += (s, e) =>
            {
                Orders = _ordersStateChangeObserver.OrdersData.ForEach(v => _ordersDataViewModelFactory.Create(v));
                Raise(() => Orders);
            };            
        }

        public IEnumerable<IOrdersDataViewModel> Orders { get; private set; }
    }
}
