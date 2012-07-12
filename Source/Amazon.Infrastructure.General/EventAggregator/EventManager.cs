using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Infrastructure.General.Interfaces.EventAggregator;
using Castle.DynamicProxy;
using MavenThought.Commons.Collections;
using MavenThought.Commons.Extensions;

namespace Amazon.Infrastructure.General.EventAggregator
{
    /// <summary>
    /// Implements <see cref="IEventManager"/>
    /// </summary>
    public class EventManager : IEventManager
    {
        /// <summary>
        /// Multi dictionary to store all the handlers byt type
        /// </summary>
        readonly IMultiDictionary<Type, BaseHandleEvents> _eventHandlers = new MultiDictionary<Type, BaseHandleEvents>();
        object _busLock;
        ConcurrentQueue<Action> _bus; 

        /// <summary>
        /// Generator of proxies
        /// </summary>
        private readonly ProxyGenerator _generator = new ProxyGenerator();
        object _eventHandlersLock;
        Task _processBusInBackground;
        object _lockRaiseEvent;
        long _totalEventsRaised;

        public EventManager()
        {
            _bus = new ConcurrentQueue<Action>();
            _busLock = new object();
            _eventHandlersLock = new object();
            _lockRaiseEvent = new object();
            _totalEventsRaised = 0;
            StartBackgroundProcessing();
        }

        /// <summary>
        /// Multi dictionary to store all the handlers byt type
        /// </summary>
        IMultiDictionary<Type, BaseHandleEvents> EventHandlers
        {
            get
            { 
                return _eventHandlers;
            }
        }

        /// <summary>
        /// Subscribe an event
        /// </summary>
        /// <typeparam name="T">Type of the event</typeparam>
        /// <param name="eventHandler">Event handler to use</param>
        public IHandleEventsOfType<T> Subscribe<T>(Action<T> eventHandler) where T : IEvent
        {
            lock (_eventHandlersLock)
            {
                var handler = new HandleEventsOfType<T>(eventHandler);

                EventHandlers.Add(typeof(T), handler);

                return handler;
            }
        }

        /// <summary>
        /// Removes the handler from the listeners collection
        /// </summary>
        /// <typeparam name="T">Type of the event</typeparam>
        /// <param name="eventHandler">Handler to remove</param>
        /// <exception cref="Exception">When the handler is not registered</exception>
        public void Unsubscribe<T>(IHandleEventsOfType<T> eventHandler) where T : IEvent
        {
            lock (_eventHandlersLock)
            {
                EventHandlers.Remove(typeof (T), (BaseHandleEvents) eventHandler);
            }
        }

        /// <summary>
        /// Raise an event
        /// </summary>
        /// <typeparam name="T">Event to raise</typeparam>
        public void RaiseEvent<T>(params Action<T>[] parms) where T : IEvent
        {
            lock (_lockRaiseEvent)
            {
                var raiseEventAction = new Action(() =>
                {
                    ICollection<BaseHandleEvents> listeners;
                    lock (_eventHandlersLock)
                    {
                        EventHandlers.TryGetValue(typeof (T), out listeners);
                    }

                    if (listeners != null)
                    {
                        var proxy = CreateProxy<T>();

                        parms.AsEnumerable().ForEach(action => action(proxy));

                        listeners.ForEach(listener => listener.Invoke(proxy));
                    }
                });

                _bus.Enqueue(raiseEventAction);
            }
        }

        private void StartBackgroundProcessing()
        {
            _processBusInBackground = Task.Factory.StartNew(ProcessBus);
        }

        void ProcessBus()
        {
            Action raiseEventAction;
            while(true)
            {
                if(_bus.TryDequeue(out raiseEventAction))
                {
                    if (_totalEventsRaised < long.MaxValue / 2)
                    {
                        _totalEventsRaised++;
                    }
                    Task.Factory.StartNew(raiseEventAction);
                }
                else
                {
                    System.Threading.Thread.Sleep(50);
                }
            }
        }

        private T CreateProxy<T>()
        {
            lock (this)
            {
                return (T) _generator.CreateInterfaceProxyWithoutTarget(typeof (T),
                                                                     new IInterceptor[] {new PropertyInterceptor()});
            }
        }

        internal abstract class BaseHandleEvents
        {
            public abstract void Invoke(object proxyEvent);
        }

        internal class HandleEventsOfType<T> : BaseHandleEvents, IHandleEventsOfType<T> where T : IEvent
        {
            private readonly Action<T> _eventHandler;

            public HandleEventsOfType(Action<T> eventHandler)
            {
                _eventHandler = eventHandler;
            }

            public override void Invoke(object proxyEvent)
            {
                Handle((T)proxyEvent);
            }

            public void Handle(T @event)
            {
                _eventHandler(@event);
            }
        }
    }
}

