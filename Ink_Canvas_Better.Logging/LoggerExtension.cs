using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging;

public static class LoggerExtension
{
    public static void WriteLog(this ILogger logger, LogLevel logLevel,  string? message, params object?[] args) =>
        logger.Log(logLevel, message, args);

    public static void WriteLog(this ILogger logger, LogLevel logLevel, Func<string?> message)
    {
        if (logger.IsEnabled(logLevel)) logger.Log(logLevel, message.Invoke());
    }

    public static void WriteLog(this ILogger logger, LogLevel logLevel, Exception? exception, string? message, params object?[] args) =>
        logger.Log(logLevel, exception, message, args);

    public static void WriteLog(this ILogger logger, LogLevel logLevel, Exception? exception, Func<string?> message)
    {
        if (logger.IsEnabled(logLevel)) logger.Log(logLevel, exception, message.Invoke());
    }
}
