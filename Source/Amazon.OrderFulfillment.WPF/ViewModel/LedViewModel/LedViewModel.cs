using Amazon.OrderFulfillment.Domain;

namespace Amazon.OrderFulfillment.WPF.ViewModel.LedViewModel
{
    public class LedViewModel 
    {
        public ConfigState State { get; set; }

        public LedViewModel(ConfigState state)
        {
            State = state;
        }
    }

}
