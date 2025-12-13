using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;
using Ink_Canvas_Better.Controls.FloatingBar;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Pages.Settings.Home;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better
{
    class Program
    {
        public static IHost Host;

        /// <summary>
        /// StartupArgs:
        /// <list type="bullet">
        /// -m multiple
        /// </list>
        /// </summary>
        public static string[]? StartupArgs { get; set; } = null;
        public static string RootPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + "\\Ink Canvas Better\\";

        /// <summary>
        /// Used for check
        /// </summary>
        public readonly static ConcurrentDictionary<string, Type> RegisteredControls = new();

        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        public static void Main(string[] args)
        {
            StartupArgs = args;
            Init();
            GetService<App>().Run();
        }

        public static void Init()
        {
            Host = Microsoft.Extensions.Hosting.Host.
                CreateDefaultBuilder().
                ConfigureServices((context, service) =>
                {
                    // Services
                    service.AddSingleton<SettingsService>();
                    service.AddSingleton<ControlsService>();
                    service.AddSingleton<InkCanvasService>();
                    service.AddSingleton<ThemeService>();
                    // UI
                    service.AddSingleton<App>();
                    service.AddSingleton<MainWindow>();
                    service.AddSingleton<SettingsWindow>();
                    service.AddSingleton<LanguageWindow>();
                    // Pages
                    service.AddSingleton<HomePage>();
                    // FloatingBarComponent
                    RegComponents<FloatingBar>(FloatingBar.Guid);
                    RegComponents<FloatingBarGroup>(FloatingBarGroup.Guid);
                    RegComponents<MultifunctionControl>(MultifunctionControl.Guid);
                    RegComponents<SettingsControl>(SettingsControl.Guid);

                    void RegComponents<T>(string guid)
                    {
                        if (RegisteredControls.ContainsKey(guid) | !RegisteredControls.TryAdd(guid, typeof(T)))
                        {
                            Debug.WriteLine($"Component with guid {{{guid}}} failed to register");
                            return;
                        }
                        service.AddTransient(typeof(T));
                    }
                }).
                ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddCompositeLogger((configuration) =>
                    {
                        configuration.Loggers =
                        [
                            new FileLogger(() => new FileLoggerConfiguration() { MinimumLogLevel = LogLevel.Information } ),
#if DEBUG
                            new ConsoleLogger(() => new ConsoleLoggerConfiguration() { MinimumLogLevel = LogLevel.Debug, OutputTarget = OutputTarget.Debug } ),
#endif
                        ];
                    });
                }).
                Build();
        }

        public static object GetService(Type t)
        {
            var s = Host?.Services.GetService(t);
            if (s != null)
            {
                return s;
            }
            throw new ArgumentException($"Service {s} is null!");
        }

        public static object? TryGetService(Type t)
        {
            return Host?.Services.GetService(t);
        }

        public static T GetService<T>()
        {
            var s = Host?.Services.GetService(typeof(T));
            if (s != null)
            {
                return (T)s;
            }
            throw new ArgumentException($"Service {typeof(T)} is null!");
        }

        public static T? TryGetService<T>()
        {
            return (T?)Host?.Services.GetService(typeof(T));
        }
    }
}
