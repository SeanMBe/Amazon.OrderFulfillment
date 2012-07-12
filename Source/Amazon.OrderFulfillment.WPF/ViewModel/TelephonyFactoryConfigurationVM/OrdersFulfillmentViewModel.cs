using System.Windows.Input;
using Amazon.OrderFulfillment.WPF.Common;
using Amazon.OrderFulfillment.WPF.ViewModel.Orders;
using Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM.Commands;

namespace Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM
{
    public class OrdersFulfillmentViewModel : BaseNotifyPropertyChanged, IOrdersFulfillmentViewModel
    {
        IOrdersDataViewModel _ordersDataViewModel;
        string _orderFulfillmentMessage;

        public OrdersFulfillmentViewModel(IOrdersDataViewModel ordersDataViewModel, IStartOrderCommand startOrderCommand)
        {
            Orders = ordersDataViewModel;
            object lockStartOrderCommand = new object();
            this.OrderFulfillmentMessage = "Please Start an Order";
            this.StartOrder = startOrderCommand;
            
            this.StartOrder.CanExecuteChanged += (a, b) =>
            {
                lock (lockStartOrderCommand)
                {
                    if (!this.StartOrder.CanExecute(null))
                    {
                        OrderFulfillmentMessage = "Order Fulfillent in Process";
                    }
                    this.Raise(() => StartOrderEnabled);
                }
            };

        }

        public IOrdersDataViewModel Orders
        {
            get
            {
                return _ordersDataViewModel;
            }
            set 
            { 
                _ordersDataViewModel = value;
                Raise(() => Orders);
            }
        }

        public string OrderFulfillmentMessage
        {
            get
            {
                return _orderFulfillmentMessage;
            }
            private set 
            { 
                _orderFulfillmentMessage = value;
                Raise(() => OrderFulfillmentMessage);
            }
        }

        public ICommand StartOrder { get; private set; }

        public bool StartOrderEnabled { get { return StartOrder.CanExecute(null); } }
    }
}