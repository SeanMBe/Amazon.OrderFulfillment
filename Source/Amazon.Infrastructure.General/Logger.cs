using Amazon.Infrastructure.General.Interfaces;

namespace Amazon.Infrastructure.General
{
    public  static class Logger
    {
        public static void Log(string message)
        {
            LogInstance.LogMessage(message);
        }

        public static ILog LogInstance { get; private set; }

        public static void Log(string stringFormat, params object[] args)
        {
            Log(string.Format(stringFormat, args));
        }

        public static void SetLogger(ILog log)
        {
            LogInstance = log;
        }

        public static void SetDefaultLoggerIfNoneIsLoaded()
        {
            if(LogInstance == null)
            {
                LogInstance = new Log();
            }
        }
    }
}
