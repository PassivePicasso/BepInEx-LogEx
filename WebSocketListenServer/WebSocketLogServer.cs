using BepInEx.Logging;
using WebSocketListenServers;
using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using WebSocketSharp.Server;

namespace PassivePicasso.WebSocket
{
    public class WebSocketLogServer 
    {
        internal static ManualLogSource logger;
        internal static WebSocketServer socketServer;

        public static IEnumerable<string> TargetDLLs => Enumerable.Empty<string>();

        public static void Patch(AssemblyDefinition assembly)
        {

        }

        // Called before patching occurs
        public static void Initialize()
        {
            logger = Logger.CreateLogSource(nameof(WebSocketLogServer));

            socketServer = new WebSocketServer("ws://localhost:5892");
            socketServer.AddWebSocketService<WebSocketListener>("/Log");
            socketServer.Start();

            logger.LogInfo($"Registered {nameof(WebSocketLogServer)}");
        }
    }
}
