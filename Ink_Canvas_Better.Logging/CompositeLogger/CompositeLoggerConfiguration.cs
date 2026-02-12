using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging;

public class CompositeLoggerConfiguration
{
    public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;

    public string LogDirectoryPath { get; set; } = "./Logs/";

    public OutputTarget OutputTarget { get; set; } = OutputTarget.File;
}

[Flags]
public enum OutputTarget
{
    Debug = 1 << 0,
    Console = 1 << 1,
    File = 1 << 2
}