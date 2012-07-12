namespace Amazon.Infrastructure.General.Interfaces.EventAggregator
{
    /// <summary>
    /// Event handler for events of type {IEvent}
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHandleEventsOfType<T> where T : IEvent
    {
        /// <summary>
        /// Handles the event
        /// </summary>
        /// <param name="event">Event instance</param>
        void Handle(T @event);
    }
}