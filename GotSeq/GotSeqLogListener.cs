using BepInEx;
using BepInEx.Logging;
using Serilog;

namespace PassivePicasso.GotSeq
{
    public class GotSeqLogListener : ILogListener
    {
        private Serilog.Core.Logger logger;

        public GotSeqLogListener()
        {
            var config = new LoggerConfiguration();
            if (string.IsNullOrEmpty(GotSeqInitializer.SeqApiKey.Value))
                config = config.WriteTo.Seq(GotSeqInitializer.SeqAddress.Value);
            else
                config = config.WriteTo.Seq(GotSeqInitializer.SeqAddress.Value, apiKey: GotSeqInitializer.SeqApiKey.Value);

            logger = config.CreateLogger();
        }

        public void Dispose()
        {
            Log.CloseAndFlush();
        }

        public void LogEvent(object sender, LogEventArgs eventArgs)
        {
            string log = "[{level}:{source}] {data}";

            switch (eventArgs.Level)
            {
                case LogLevel.Debug:
                    logger.Debug(log, eventArgs.Level, ((ILogSource)sender).SourceName, eventArgs.Data);
                    break;
                case LogLevel.Fatal:
                    logger.Fatal(log, eventArgs.Level, ((ILogSource)sender).SourceName, eventArgs.Data);
                    break;
                case LogLevel.Error:
                    logger.Error(log, eventArgs.Level, ((ILogSource)sender).SourceName, eventArgs.Data);
                    break;
                case LogLevel.Warning:
                    logger.Warning(log, eventArgs.Level, ((ILogSource)sender).SourceName, eventArgs.Data);
                    break;
                case LogLevel.Message:
                    logger.Verbose(log, eventArgs.Level, ((ILogSource)sender).SourceName, eventArgs.Data);
                    break;
                case LogLevel.Info:
                    logger.Information(log, eventArgs.Level, ((ILogSource)sender).SourceName, eventArgs.Data);
                    break;
                default: break;
            }
        }
    }
}
