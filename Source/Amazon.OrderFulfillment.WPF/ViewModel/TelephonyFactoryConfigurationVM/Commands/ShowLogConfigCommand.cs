using System;
using System.Windows;
using System.Windows.Input;

namespace Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM.Commands
{
    public class ShowLogConfigCommand : ICommand
    {
        public void Execute(object parameter)
        {
            MessageBox.Show("Log window not implemented");
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}