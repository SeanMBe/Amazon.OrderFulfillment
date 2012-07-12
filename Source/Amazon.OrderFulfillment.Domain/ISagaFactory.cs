namespace Amazon.OrderFulfillment.Domain
{
    public interface ISagaFactory
    {
        IOrderFulfillmentSaga Create(IOrder order);
    }
}
