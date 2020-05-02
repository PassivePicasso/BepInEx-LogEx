using BepInEx.Logging;
using WebSocketListenServers;
using Mono.Cecil;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace PassivePicsso
{
    public class BepinSerilogger 
    {
        public static IEnumerable<string> TargetDLLs => Enumerable.Empty<string>();

        public static void Patch(AssemblyDefinition assembly)
        {

        }

        // Called before patching occurs
        public static void Initialize()
        {
            var logger = Logger.CreateLogSource("BepinSerilogger");
            Logger.Listeners.Add(new SerilogListener());
            logger.LogInfo("Registered BepinSerilogger");
        }
    }
}
