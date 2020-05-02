using BepInEx.Logging;
using Serilog;

namespace WebSocketListenServers
{
    public class SerilogListener : ILogListener
    {
        private Serilog.Core.Logger logger;

        public SerilogListener()
        {
            logger = new LoggerConfiguration()
                                    .WriteTo.Seq("http://localhost:5341")
                                    .CreateLogger();
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
