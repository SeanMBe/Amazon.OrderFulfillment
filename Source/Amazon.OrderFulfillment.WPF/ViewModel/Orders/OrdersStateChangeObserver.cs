using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.OrderFulfillment.Domain;

namespace Amazon.OrderFulfillment.WPF.ViewModel.Orders
{
    public class OrdersStateChangeObserver : IOrdersStateChangeObserver
    {
        readonly IOrderFulfillmentSagaManager _orderFulfillmentSagaManager;
        readonly object _devicesValueLock;
        IEnumerable<IEnumerable<object>> _ordersData;
        object _lastTimeSagasChangedLock;

        public OrdersStateChangeObserver(IOrderFulfillmentSagaManager orderFulfillmentSagaManager)
        {
            _orderFulfillmentSagaManager = orderFulfillmentSagaManager;
            _devicesValueLock = new object();
            _lastTimeSagasChangedLock = new object();

            OrdersStateChanged = delegate { };

            OrdersData = Enumerable.Empty<IEnumerable<IEnumerable<object>>>();

            var lastTimeSagasChanged = DateTime.Now.Subtract(TimeSpan.FromSeconds(1));

            _orderFulfillmentSagaManager.SagasChanged += (s, e) =>
            {
                lock (_lastTimeSagasChangedLock)
                {
                    if (DateTime.Now.Subtract(lastTimeSagasChanged).TotalMilliseconds >= 500)
                    {
                        UpdateConfigurationState();
                        lastTimeSagasChanged = DateTime.Now;
                    }
                }
            };
        }

        public event Action<object, EventArgs> OrdersStateChanged;

        public IEnumerable<IEnumerable<object>> OrdersData
        {
            get
            {
                return _ordersData;
            }
            set 
            { 
                _ordersData = value;
                OrdersStateChanged(this, new EventArgs());
            }
        }

        void UpdateConfigurationState()
        {
            lock (_devicesValueLock)
            {
                var i = 1;
                
                OrdersData = _orderFulfillmentSagaManager
                    .Sagas
                    .Where(s => s.SagaData.OrderStarted != ConfigState.Unknown)
                    .Select(s => CreateValues(s.SagaData, i++));
            }
        }

        IEnumerable<object> CreateValues(IOrderFulfillmentSagaDataReadOnly sagaData, int i)
        {
            var orderState = new List<object>
            {
                ConfigState.Unknown,
                ConfigState.Unknown,
                ConfigState.Unknown,
                ConfigState.Unknown,
                ConfigState.Unknown,
                ConfigState.Unknown,
                ConfigState.Unknown,
                ConfigState.Unknown,
                ConfigState.Unknown,
                ConfigState.Unknown,
                ConfigState.Unknown
            };

            return orderState;
        }       
    }
}