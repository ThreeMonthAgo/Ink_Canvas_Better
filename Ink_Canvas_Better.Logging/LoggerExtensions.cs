using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Logging;

public static class LoggerExtensions
{
    public static ILoggingBuilder AddCompositeLogger(this ILoggingBuilder builder)
    {
        builder.Services.AddOptions();
        builder.Services.TryAddSingleton<ILoggerProvider, CompositeLoggerProvider>();
        return builder;
    }

    public static ILoggingBuilder AddCompositeLogger(
        this ILoggingBuilder builder,
        Action<CompositeLoggerConfiguration> configure)
    {
        builder.AddCompositeLogger();
        builder.Services.Configure(configure);
        return builder;
    }
}
