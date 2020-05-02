using BepInEx.Logging;
using PassivePicasso.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketListenServers
{
    public class WebSocketListener : WebSocketBehavior, ILogListener
    {
        ManualLogSource log => WebSocketLogServer.logger;

        public WebSocketListener()
        {
            log.LogInfo("WebSocket ListenServer established");
            BepInEx.Logging.Logger.Listeners.Add(this);
        }

        public void Dispose()
        {
            log.LogInfo("WebSocket ListenServer disposing");
            WebSocketLogServer.socketServer.RemoveWebSocketService("/Log");
            WebSocketLogServer.socketServer.Stop();
            BepInEx.Logging.Logger.Listeners.Remove(this);
        }

        public void LogEvent(object sender, LogEventArgs eventArgs) => Send($"[{eventArgs.Level}:{((ILogSource)sender).SourceName}] {eventArgs.Data}");

        protected override void OnClose(CloseEventArgs e)
        {
            log.LogInfo("Logging Client connection closed");
            base.OnClose(e);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            log.LogInfo("Logging Client connection error");
            base.OnError(e);
        }

        protected override void OnOpen()
        {
            log.LogInfo("Logging Client connection open");
            base.OnOpen();
        }
    }
}
