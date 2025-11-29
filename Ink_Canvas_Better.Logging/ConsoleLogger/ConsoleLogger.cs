using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging;
public sealed class ConsoleLogger(Func<ConsoleLoggerConfiguration> getCurrentConfig) : ILogger
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= getCurrentConfig().MinimumLogLevel;

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        ConsoleLoggerConfiguration config = getCurrentConfig();
        if (config.EventId == 0 && config.EventId == eventId.Id)
        {
            Task.Run(() =>
            {
                if (config.OutputTarget.HasFlag(OutputTarget.Console))
                {
                    Console.WriteLine(string.Format("=> {0} [{1}] {2} {3}", DateTime.Now.ToString("HH:mm:ss.fff"), logLevel, state, exception));
                }
                if (config.OutputTarget.HasFlag(OutputTarget.Debug))
                {
                    Debug.WriteLine(string.Format("=> {0} [{1}] {2} {3}", DateTime.Now.ToString("HH:mm:ss.fff"), logLevel, state, exception));
                }
            });
        }
    }

}
