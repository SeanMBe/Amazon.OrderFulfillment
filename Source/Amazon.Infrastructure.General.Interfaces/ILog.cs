using System.Collections.Generic;

namespace Amazon.Infrastructure.General.Interfaces
{
    public interface ILog
    {
        void LogMessage(string message);

        void LogMessage(string stringFormat, params object[] args);

        event LogEvent.LogEventHandler NewLogMessage;
        
        IEnumerable<string> Messages { get; }
    }
}