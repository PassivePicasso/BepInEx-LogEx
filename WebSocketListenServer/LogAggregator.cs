﻿using BepInEx.Logging;
using PassivePicasso.WebSocket;
using System.Collections.Generic;

namespace WebSocketListenServer
{
    public class LogAggregator : ILogListener
    {
        List<string> LogEntries = new List<string>(5000);
        internal IEnumerable<string> Logs => LogEntries.AsReadOnly();

        public void Dispose()
        {
        }

        public void LogEvent(object sender, LogEventArgs eventArgs)
        {
            LogEntries.Add(WebSocketLogServer.Format(sender, eventArgs));
        }
    }
}
