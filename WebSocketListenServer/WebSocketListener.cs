using BepInEx.Logging;
using PassivePicasso.WebSocket;
using System.Linq;
using WebSocketSharp;
using WebSocketSharp.Server;
using Logger = BepInEx.Logging.Logger;

namespace WebSocketListenServers
{
    public class WebSocketListener : WebSocketBehavior, ILogListener
    {
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
                Send(WebSocketLogServer.Format(sender, eventArgs));
        }

        protected override void OnOpen()
        {
            if (this.State == WebSocketState.Open)
                foreach (var log in WebSocketLogServer.LogAggregator.Logs) Send(log);
        }
    }
}
