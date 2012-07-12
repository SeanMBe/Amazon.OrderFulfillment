using System;

namespace Amazon.Infrastructure.General.Interfaces
{
    public class LogEventArgs : EventArgs
    {
        public LogEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}