using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging
{
    public sealed class ConsoleLoggerConfiguration
    {
        public int EventId { get; set; }

        public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;

        public OutputTarget OutputTarget { get; set; } = OutputTarget.Console;
    }

    [Flags]
    public enum OutputTarget
    {
        Console = 1 << 0,
        Debug = 1 << 1
    }
}
