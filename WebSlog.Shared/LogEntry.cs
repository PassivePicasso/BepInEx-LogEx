using BepInEx.Logging;
using ProtoBuf;

namespace PassivePicasso.WebSlog.Shared
{

    [ProtoContract]
    public class LogEntry
    {
        [ProtoMember(1)]
        public string Source { get; internal set; }

        [ProtoMember(2)]
        public string Level { get; internal set; }

        [ProtoMember(3)]
        public int LevelCode { get; internal set; }

        [ProtoMember(4)]
        public string Data { get; internal set; }

        public static explicit operator LogEntry(LogEventArgs b) => new LogEntry
        {
            Source = b.Source.SourceName,
            Level = $"{b.Level}",
            LevelCode = (int)b.Level,
            Data = $"{b.Data}"
        };
    }
}
