using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging;

public sealed class FileLoggerConfiguration
{
    public int EventId { get; set; }

    public string LogDirectoryPath { get; set; } = "./Logs/";

    public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;
}
