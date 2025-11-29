using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ink_Canvas_Better.Logging;

public sealed class CompositeLoggerProvider(IOptionsMonitor<CompositeLoggerConfiguration> config) : ILoggerProvider
{
    private CompositeLoggerConfiguration _currentConfig = config.CurrentValue;
    private readonly ConcurrentDictionary<string, CompositeLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new CompositeLogger(GetCurrentConfig));

    private CompositeLoggerConfiguration GetCurrentConfig() => _currentConfig;

    public void Dispose() => _loggers.Clear();
}

public sealed class FileLoggerProvider(IOptionsMonitor<FileLoggerConfiguration> config) : ILoggerProvider
{
    private FileLoggerConfiguration _currentConfig = config.CurrentValue;
    private readonly ConcurrentDictionary<string, FileLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new FileLogger(GetCurrentConfig));

    private FileLoggerConfiguration GetCurrentConfig() => _currentConfig;

    public void Dispose() => _loggers.Clear();
}

public sealed class ConsoleLoggerProvider(IOptionsMonitor<ConsoleLoggerConfiguration> config) : ILoggerProvider
{
    private ConsoleLoggerConfiguration _currentConfig = config.CurrentValue;
    private readonly ConcurrentDictionary<string, ConsoleLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new ConsoleLogger(GetCurrentConfig));

    private ConsoleLoggerConfiguration GetCurrentConfig() => _currentConfig;

    public void Dispose() => _loggers.Clear();
}


