using System;
using System.Collections.Generic;
using System.Text;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Ink_Canvas_Better.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ink_Canvas_Better.Controls.FloatingBar;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

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
                    // UI
                    service.AddSingleton<App>();
                    service.AddSingleton<MainWindow>();
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
            RegisterControls();
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

        /// <summary>
        /// Register custom controls here.
        /// </summary>
        private static void RegisterControls()
        {
            ControlsService controlsService = GetService<ControlsService>();
            controlsService.TryRegisterControl<FloatingBar>(FloatingBar.Guid);
            controlsService.TryRegisterControl<FloatingBarGroup>(FloatingBarGroup.Guid);
            controlsService.TryRegisterControl<MultifunctionControl>(MultifunctionControl.Guid);
            controlsService.TryRegisterControl<SettingsControl>(SettingsControl.Guid);
            //controlsService.TryRegisterControl<CursorControl>(CursorControl.ControlGuid);
            //controlsService.TryRegisterControl<PenControl>(PenControl.ControlGuid);
        }
    }
}
