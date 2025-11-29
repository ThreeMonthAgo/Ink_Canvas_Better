using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging;

public sealed class FileLogger(Func<FileLoggerConfiguration> getCurrentConfig) : ILogger
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

        FileLoggerConfiguration config = getCurrentConfig();
        if (config.EventId == 0 || config.EventId == eventId.Id)
        {
            Task.Run(() =>
            {
                if (!Directory.Exists(config.LogDirectoryPath))
                {
                    Directory.CreateDirectory(config.LogDirectoryPath);
                }
                StreamWriter sw = new(config.LogDirectoryPath + $"{DateTime.Now:yyyy-MM-dd}.log", true);
                sw.WriteLine(string.Format("{0} [{1}] {2} {3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), logLevel, state, exception));
                sw.Close();
            });
        }
    }
}
