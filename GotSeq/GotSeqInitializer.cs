using BepInEx.Configuration;
using BepInEx.Logging;
using Mono.Cecil;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PassivePicasso.GotSeq
{
    public class GotSeqInitializer
    {
        public static IEnumerable<string> TargetDLLs => Enumerable.Empty<string>();

        public static ConfigEntry<string> SeqAddress { get; set; }
        public static ConfigEntry<string> SeqApiKey { get; set; }
        
        public static void Patch(AssemblyDefinition assembly)
        {

        }

        // Called before patching occurs
        public static void Initialize()
        {
            var rootPath = BepInEx.Paths.ConfigPath;
            var configPath = Path.Combine(rootPath, "GotSeq.cfg");
            var config = new ConfigFile(configPath, true);
            SeqAddress = config.Bind<string>("Seq.NET", nameof(SeqAddress), "http://localhost:5341");
            SeqApiKey = config.Bind<string>("Seq.NET", nameof(SeqApiKey), "");

            var logger = Logger.CreateLogSource("GotSeq");
            Logger.Listeners.Add(new GotSeqLogListener());
            logger.LogInfo("Registered GotSeq? log forwarder");
        }
    }
}
