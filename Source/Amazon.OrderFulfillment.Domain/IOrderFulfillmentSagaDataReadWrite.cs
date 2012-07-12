namespace Amazon.OrderFulfillment.Domain
{
    public interface IOrderFulfillmentSagaDataReadWrite : IOrderFulfillmentSagaDataReadOnly
    {
        void SetOrderStartedGood();

        void SetSagaPending();

        void SetSagaComplete();
        
    }
}
