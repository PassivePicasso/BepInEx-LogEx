using BepInEx.Logging;
using ProtoBuf;
using System;
using System.IO;
using WebSocketSharp;
using WebSocketSharp.Server;
using Logger = BepInEx.Logging.Logger;

namespace PassivePicasso.WebSlog
{
    using Shared;

    public class WebSocketListener : WebSocketBehavior, ILogListener
    {

        static byte[] empty = new byte[0];
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
            try
            {
                if (this.State == WebSocketState.Open)
                    Send(Serialize(eventArgs));
            }
            catch (Exception e)
            {
                log.LogError(e);
            }
        }

        private byte[] Serialize(LogEventArgs eventArgs)
        {
            try
            {
                using (var stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, (LogEntry)eventArgs);
                    stream.Seek(0, SeekOrigin.Begin);
                    return stream.ToArray();
                }
            }
            catch (Exception e)
            {
                log.LogError(e);
            }

            return empty;

        }

        protected override void OnOpen()
        {
            try
            {
                if (this.State == WebSocketState.Open)
                    foreach (var log in WebSocketLogServer.LogAggregator.Logs)
                        Send(Serialize(log));
            }
            catch (Exception e)
            {
                log.LogError(e);
            }
        }
    }
}
