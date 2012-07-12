using System;
using Amazon.Infrastructure.General.Interfaces.EventAggregator;
using Amazon.OrderFulfillment.Domain;

namespace Amazon.OrderFulfillment.WPF.ViewModel.TelephonyFactoryConfigurationVM.Commands
{
    public class StartOrderCommand : IStartOrderCommand
    {
        readonly IEventManager _eventManager;
        readonly IOrderFactory _orderFactory;

        public StartOrderCommand(IEventManager eventManager, IOrderFactory orderFactory)
        {
            _eventManager = eventManager;
            _orderFactory = orderFactory;
        }

        public void Execute(object parameter)
        {
            var order = _orderFactory.Create();
            _eventManager.RaiseEvent<IEventOrderStarted>(args => args.Order = order);
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}