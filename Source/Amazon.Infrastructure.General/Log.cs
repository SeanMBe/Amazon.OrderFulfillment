using System;
using System.Collections.Generic;
using System.Diagnostics;
using Amazon.Infrastructure.General.Interfaces;

namespace Amazon.Infrastructure.General
{
    public class Log : ILog
    {
        private List<string> _messages;

        public Log()
        {
            _messages = new List<string>();
            NewLogMessage = delegate {};
        }

        public IEnumerable<string> Messages { get { return _messages; } }

        public void LogMessage(string message)
        {
            var stackTrace = new StackTrace(true);
            var oneStackBack = stackTrace.GetFrame(stackTrace.FrameCount - 2);
            
            var outMessage =  ">> " + oneStackBack.GetFileName() + " -- method: " + oneStackBack.GetMethod() + " -- file number: " + oneStackBack.GetFileLineNumber() + " -- message: "  + message + Environment.NewLine;
            
            _messages.Add(outMessage);

            NewLogMessage(this, new LogEventArgs(outMessage));
        }

        public void LogMessage(string stringFormat, params object[] args)
        {
            LogMessage(string.Format(stringFormat, args));
        }

        public event LogEvent.LogEventHandler NewLogMessage;
    }
}