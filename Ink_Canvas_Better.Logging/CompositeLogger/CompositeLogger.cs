using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging;

public sealed class CompositeLogger(Func<CompositeLoggerConfiguration> getCurrentConfig) : ILogger
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

        var config = getCurrentConfig();
        if (config.OutputTarget.HasFlag(OutputTarget.Debug))
        {
            Debug.WriteLine(string.Format("==> {0} [{1}] {2} {3}", DateTime.Now.ToString("HH:mm:ss.fff"), logLevel, state, exception));
        }
        if (config.OutputTarget.HasFlag(OutputTarget.Console))
        {
            Console.WriteLine(string.Format("==> {0} [{1}] {2} {3}", DateTime.Now.ToString("HH:mm:ss.fff"), logLevel, state, exception));
        }
        if (config.OutputTarget.HasFlag(OutputTarget.File))
        {
            if (!Directory.Exists(config.LogDirectoryPath))
            {
                Directory.CreateDirectory(config.LogDirectoryPath);
            }
            StreamWriter sw = new(
                path: config.LogDirectoryPath + $"{DateTime.Now:yyyy-MM-dd}.log",
                encoding: System.Text.Encoding.UTF8,
                options: new FileStreamOptions()
                {
                    Mode = FileMode.Append,
                    Share = FileShare.ReadWrite,
                    Access = FileAccess.Write
                });
            sw.WriteLine(string.Format("{0} [{1}] {2} {3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), logLevel, state, exception));
            sw.Close();
        }
    }
}
