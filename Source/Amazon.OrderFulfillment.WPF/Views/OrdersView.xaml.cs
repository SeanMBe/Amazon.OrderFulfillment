using System.ComponentModel;
using System.Windows;
using Amazon.OrderFulfillment.WPF.IOCContainer;
using Amazon.OrderFulfillment.WPF.ViewModel.Orders;

namespace Amazon.OrderFulfillment.WPF.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationView.xaml
    /// </summary>
    public partial class OrdersView : INotifyPropertyChanged
    {
        public OrdersView() : this(IOC.ServiceLocator.GetInstance<IOrdersViewModel>())
        {
        }

        public OrdersView(IOrdersViewModel deviceConfigurationViewModel)
        {
            InitializeComponent();
            this.DataContext = deviceConfigurationViewModel;
        }

        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Orders", typeof(IOrdersViewModel), typeof(OrdersView), new UIPropertyMetadata(
                                            null,
                                            (d, e) =>
                                            {
                                                var self = (OrdersView)d;
                                                ViewChanged(self);
                                            }));

        private static void ViewChanged(OrdersView self)
        {
            if (self.PropertyChanged != null)
            {
                self.PropertyChanged(self, new PropertyChangedEventArgs("Orders"));
            }
        }

        public IOrdersDataViewModel Orders
        {
            get
            {
                return (IOrdersDataViewModel)GetValue(OrdersProperty);
            }
            set
            {
                SetValue(OrdersProperty, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
