using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ink_Canvas_Better.Logging;

#if DEBUG

public sealed class DebugLoggerProvider(IOptionsMonitor<DebugLoggerConfiguration> config) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new DebugLogger(() => config.CurrentValue);

    public void Dispose() { }
}

public sealed class DebugLogger(Func<DebugLoggerConfiguration> getCurrentConfig) : ILogger
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

        Debug.WriteLine(string.Format("==> {0} [{1}] {2} {3}", DateTime.Now.ToString("HH:mm:ss.fff"), logLevel, state, exception));
    }
}

public static class DebugLoggerExtensions
{
    public static ILoggingBuilder AddDebugLogger(this ILoggingBuilder builder)
    {
        builder.Services.AddOptions();
        builder.Services.AddSingleton<ILoggerProvider, DebugLoggerProvider>();
        return builder;
    }

    public static ILoggingBuilder AddDebugLogger(
        this ILoggingBuilder builder,
        Action<DebugLoggerConfiguration> configure)
    {
        builder.AddDebugLogger();
        builder.Services.Configure(configure);
        return builder;
    }
}

public class DebugLoggerConfiguration
{
    public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;
}

#endif