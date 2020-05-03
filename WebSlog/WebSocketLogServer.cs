using BepInEx.Logging;
using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp.Server;

namespace PassivePicasso.WebSlog
{
    public class WebSocketLogServer
    {
        internal static string Format(object sender, LogEventArgs eventArgs) => $"[{eventArgs.Level}:{((ILogSource)sender).SourceName}] {eventArgs.Data}";

        internal static ManualLogSource logger;
        internal static WebSocketServer socketServer;
        internal static LogAggregator LogAggregator;
        public static IEnumerable<string> TargetDLLs => Enumerable.Empty<string>();

        public static void Patch(AssemblyDefinition assembly)
        {

        }

        // Called before patching occurs
        public static void Initialize()
        {
            logger = Logger.CreateLogSource(nameof(WebSocketLogServer));

            LogAggregator = new LogAggregator();
            Logger.Listeners.Add(LogAggregator);

            socketServer = new WebSocketServer("ws://localhost:5892");
            socketServer.AddWebSocketService<WebSocketListener>("/Log");
            
            socketServer.Start();


            logger.LogInfo($"Registered {nameof(WebSocketLogServer)}");
        }
    }
}
