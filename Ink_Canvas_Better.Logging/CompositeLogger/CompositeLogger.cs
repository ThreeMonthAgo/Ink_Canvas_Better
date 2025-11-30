using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging;

public sealed class CompositeLogger(Func<CompositeLoggerConfiguration> getCurrentConfig) : ILogger
{

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        var _loggers = getCurrentConfig().Loggers;
        foreach (var logger in _loggers)
        {
            Task.Run(() =>
            {
                try
                {
                    logger.Log(logLevel, eventId, state, exception, formatter);
                }
                catch (Exception ex)
                {
#if DEBUG
                    Debug.WriteLine($"Logger failed: {ex.Message}");
#endif
                }
            });
        }
    }
}
