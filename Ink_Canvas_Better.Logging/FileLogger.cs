using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ink_Canvas_Better.Logging;

public sealed class FileLoggerProvider(IOptionsMonitor<FileLoggerConfiguration> config) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new FileLogger(() => config.CurrentValue);

    public void Dispose() { }
}

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
        if (!IsEnabled(logLevel)) return;

        var config = getCurrentConfig();
        StreamWriter sw = new(
            config.LogDirectoryPath + $"{DateTime.Now:yyyy-MM-dd}.log",
            Encoding.UTF8,
            new FileStreamOptions()
            {
                Mode = FileMode.Append,
                Share = FileShare.ReadWrite,
                Access = FileAccess.Write
            });
        sw.WriteLine(string.Format("{0} [{1}] {2} {3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), logLevel, state, exception));
        sw.Close();
    }
}

public static class FileLoggerExtensions
{
    public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder)
    {
        builder.Services.AddOptions();
        builder.Services.AddSingleton<ILoggerProvider, FileLoggerProvider>();
        return builder;
    }

    public static ILoggingBuilder AddFileLogger(
        this ILoggingBuilder builder,
        Action<FileLoggerConfiguration> configure)
    {
        builder.AddFileLogger();
        builder.Services.Configure(configure);
        return builder;
    }
}

public class FileLoggerConfiguration
{
    public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;

    private string FullPath = "./Logs/";

    public string LogDirectoryPath
    {
        get => FullPath;
        set
        {
            if (Path.IsPathRooted(value))
            {
                FullPath = value;
            }
            else
            {
                FullPath = Path.GetFullPath(value);
            }

            if (!Directory.Exists(value))
            {
                Directory.CreateDirectory(value);
            }
        }
    }
}
