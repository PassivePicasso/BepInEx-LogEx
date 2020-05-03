using BepInEx.Logging;
using Mono.Cecil;
using System.Collections.Generic;
using System.Linq;
using WebSocketListenServers;

namespace PassivePicsso
{
    public class GotSeqInitializer
    {
        public static IEnumerable<string> TargetDLLs => Enumerable.Empty<string>();

        public static void Patch(AssemblyDefinition assembly)
        {

        }

        // Called before patching occurs
        public static void Initialize()
        {
            var logger = Logger.CreateLogSource("BepinSerilogger");
            Logger.Listeners.Add(new GotSeqLogListener());
            logger.LogInfo("Registered BepinSerilogger");
        }
    }
}
