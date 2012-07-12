namespace Amazon.OrderFulfillment.Domain
{
    public interface IOrderFulfillmentSaga
    {
        void Handle(IEventOrderStarted theEvent);

        string SagaId { get; }
        
        IOrderFulfillmentSagaDataReadOnly SagaData { get; }
    }
}
