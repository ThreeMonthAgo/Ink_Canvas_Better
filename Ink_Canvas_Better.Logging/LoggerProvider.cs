using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ink_Canvas_Better.Logging;

public sealed class CompositeLoggerProvider(IOptionsMonitor<CompositeLoggerConfiguration> config) : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new CompositeLogger(() => config.CurrentValue);

    public void Dispose() { }
}
