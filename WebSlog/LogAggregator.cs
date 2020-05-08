using BepInEx.Logging;
using System.Collections.Generic;

namespace PassivePicasso.WebSlog
{
    public class LogAggregator : ILogListener
    {
        List<LogEventArgs> LogEntries = new List<LogEventArgs>(5000);
        internal IEnumerable<LogEventArgs> Logs => LogEntries.AsReadOnly();

        public void Dispose()
        {
        }

        public void LogEvent(object sender, LogEventArgs eventArgs)
        {
            LogEntries.Add(eventArgs);
        }
    }
}
