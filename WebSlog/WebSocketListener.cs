using BepInEx.Logging;
using WebSocketSharp;
using WebSocketSharp.Server;
using Logger = BepInEx.Logging.Logger;
using UnityEngine;

namespace PassivePicasso.WebSlog
{
    public class WebSocketListener : WebSocketBehavior, ILogListener
    {
        [System.Serializable]
        public struct LogEntry
        {
            public string source;
            public string level;
            public string data;
            public byte levelcode;
        }
        ManualLogSource log => WebSocketLogServer.logger;

        public WebSocketListener()
        {
            Logger.Listeners.Add(this);
            log.LogInfo("WebSocket ListenServer established");
        }

        public void Dispose()
        {
            log.LogInfo("WebSocket ListenServer disposing");
        }

        public void LogEvent(object sender, LogEventArgs eventArgs)
        {
            if (this.State == WebSocketState.Open)
                    Send(Jsonify(eventArgs));
        }

        protected override void OnOpen()
        {
            if (this.State == WebSocketState.Open)
                foreach (var log in WebSocketLogServer.LogAggregator.Logs)
                {
                    Send(Jsonify(log));
                }
        }


        string Jsonify(LogEventArgs entry)
        {
            return $"{{ \"source\": \"{entry.Source.SourceName}\", \"level\": \"{entry.Level}\", \"levelcode\": \"{(byte)entry.Level}\", \"data\": \"{entry.Data}\" }}";
        }
    }
}
