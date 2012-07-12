using System;
using System.Collections.Generic;
using Amazon.Infrastructure.General.Interfaces;

namespace Amazon.Infrastructure.General
{
    public class Timer : ITimer
    {
        readonly long _frequencyInMilliseconds;
        IList<System.Timers.Timer> _timers;

        /// <summary>
        /// Construct a timer that will perform actions every certain milliseconds
        /// </summary>
        /// <param name="frequencyInMilliseconds"></param>
        public Timer(long frequencyInMilliseconds)
        {
            _frequencyInMilliseconds = frequencyInMilliseconds;
            _timers = new List<System.Timers.Timer>();
            new List<Action>();
        }

        public void RegisterCallBack(Action action)
        {
            lock (_timers)
            {
                var timer = new System.Timers.Timer(_frequencyInMilliseconds);
                timer.AutoReset = false;
                timer.Elapsed += (s, e) =>
                {
                    timer.Stop();
                    action();
                    timer.Start();
                };
                _timers.Add(timer);
                timer.Start();
            }
        }
    }
}
