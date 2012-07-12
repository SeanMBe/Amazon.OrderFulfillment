using Amazon.OrderFulfillment.WPF.IOCContainer;
using Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM;

namespace Amazon.OrderFulfillment.WPF.Views
{
    /// <summary>
    /// Interaction logic for FactoryConfigurationView.xaml
    /// </summary>
    public partial class OrderFulfillmentView
    {
        public OrderFulfillmentView() : this(IOC.ServiceLocator.GetInstance<IOrdersFulfillmentViewModel>())
        {
        }

        public OrderFulfillmentView(IOrdersFulfillmentViewModel ordersFulfillmentViewModel)
        {
            InitializeComponent();
            this.DataContext = ordersFulfillmentViewModel;
        }

    }
}
