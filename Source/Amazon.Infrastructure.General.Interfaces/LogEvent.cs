namespace Amazon.Infrastructure.General.Interfaces
{
    public abstract class LogEvent
    {
        public delegate void LogEventHandler(object sender, LogEventArgs data);
    }
}