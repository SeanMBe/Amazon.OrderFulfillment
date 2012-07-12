using System.ComponentModel;
using System.Windows;
using Amazon.OrderFulfillment.Domain;

namespace Amazon.OrderFulfillment.WPF.Views
{
    /// <summary>
    /// Interaction logic for LedView.xaml
    /// </summary>
    public partial class LedView : INotifyPropertyChanged
    {
        public LedView()
        {
            InitializeComponent();   
        }

        public static readonly DependencyProperty LedStateProperty =
            DependencyProperty.Register("LedState", typeof(ConfigState), typeof(LedView),         new UIPropertyMetadata(
                                            ConfigState.Unknown,
                                            (d, e) =>
                                            {
                                                var self = (LedView)d;
                                                ViewChanged(self);
                                            }));


        private static void ViewChanged(LedView self)
        {
            if (self.PropertyChanged != null)
            {
                self.PropertyChanged(self, new PropertyChangedEventArgs("LedState"));
            }
        }


        public ConfigState LedState
        {
            get
            {
                return (ConfigState)GetValue(LedStateProperty);
            }
            set
            {
                SetValue(LedStateProperty, value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
