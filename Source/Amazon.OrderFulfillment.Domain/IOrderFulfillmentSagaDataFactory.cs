namespace Amazon.OrderFulfillment.Domain
{
    public interface IOrderFulfillmentSagaDataFactory
    {
        IOrderFulfillmentSagaDataReadWrite Create(IOrder order);
    }
}