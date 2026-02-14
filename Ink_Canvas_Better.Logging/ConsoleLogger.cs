using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ink_Canvas_Better.Logging;

public sealed class ConsoleLoggerProvider(IOptionsMonitor<ConsoleLoggerConfiguration> config) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new ConsoleLogger(() => config.CurrentValue);

    public void Dispose() { }
}

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
        if (!IsEnabled(logLevel)) return;

        Console.WriteLine(string.Format("==> {0} [{1}] {2} {3}", DateTime.Now.ToString("HH:mm:ss.fff"), logLevel, state, exception));
    }
}

public static class ConsoleLoggerExtensions
{
    public static ILoggingBuilder AddConsoleLogger(this ILoggingBuilder builder)
    {
        builder.Services.AddOptions();
        builder.Services.TryAddSingleton<ILoggerProvider, ConsoleLoggerProvider>();
        return builder;
    }

    public static ILoggingBuilder AddConsoleLogger(
        this ILoggingBuilder builder,
        Action<ConsoleLoggerConfiguration> configure)
    {
        builder.AddConsoleLogger();
        builder.Services.Configure(configure);
        return builder;
    }
}

public class ConsoleLoggerConfiguration
{
    public LogLevel MinimumLogLevel { get; set; } = LogLevel.Information;
}
