using System;

namespace Amazon.Infrastructure.General.Interfaces.EventAggregator
{
    /// <summary>
    /// Interface for event manager
    /// </summary>
    public partial interface IEventManager
    {
        /// <summary>
        /// Subscribe an event
        /// </summary>
        /// <typeparam name="T">Type of the event</typeparam>
        /// <param name="eventHandler">Event handler to use</param>
        IHandleEventsOfType<T> Subscribe<T>(Action<T> eventHandler) where T : IEvent;

        /// <summary>
        /// Removes the handler from the listeners collection
        /// </summary>
        /// <typeparam name="T">Type of the event</typeparam>
        /// <param name="eventHandler">Handler to remove</param>
        /// <exception cref="Exception">When the handler is not registered</exception>
        void Unsubscribe<T>(IHandleEventsOfType<T> eventHandler) where T : IEvent;

        /// <summary>
        /// Raise an event
        /// </summary>
        /// <typeparam name="T">Event to raise</typeparam>
        void RaiseEvent<T>(params Action<T>[] parms) where T : IEvent;
    }
}