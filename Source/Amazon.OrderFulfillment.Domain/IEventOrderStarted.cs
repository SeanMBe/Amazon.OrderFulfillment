using Amazon.Infrastructure.General.Interfaces.EventAggregator;

namespace Amazon.OrderFulfillment.Domain
{
    public interface IEventOrderStarted : IEvent
    {
        IOrder Order { get; set; }
    }
}