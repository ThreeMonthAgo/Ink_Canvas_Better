using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace Ink_Canvas_Better.Logging;

public static class LoggerExtensions
{
    public static ILoggingBuilder AddCompositeLogger(
        this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, CompositeLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <CompositeLoggerConfiguration, CompositeLoggerProvider>(builder.Services);

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


    #region FileLogger

    public static ILoggingBuilder AddFileLogger(
        this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <FileLoggerConfiguration, FileLoggerProvider>(builder.Services);

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

    #endregion

    #region ConsoleLogger

    public static ILoggingBuilder AddConsoleLogger(
        this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services.TryAddEnumerable(
            ServiceDescriptor.Singleton<ILoggerProvider, ConsoleLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <ConsoleLoggerConfiguration, ConsoleLoggerProvider>(builder.Services);

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

    #endregion
}
