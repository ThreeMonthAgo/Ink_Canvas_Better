using System.IO;
using Ink_Canvas_Better.Controls.FloatingBarControls;
using Ink_Canvas_Better.Controls.Panel;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better
{
    public static class AppHost
    {
        private static IHost Host;

        public static void Init()
        {
            AppHost.Host = Microsoft.Extensions.Hosting.Host.
                CreateDefaultBuilder().
                ConfigureServices((context, service) =>
                {
                    // Services
                    service.AddSingleton<ControlsService>();
                    service.AddSingleton<SettingsService>();
                    service.AddSingleton<InkCanvasService>();
                    // UI
                    service.AddSingleton<MainWindow>();
                    service.AddSingleton<FloatingBar>();
                }).
                ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddFileLogger(configuration =>
                    {
                        configuration.LogDirectoryPath = Path.Combine(App.RootPath, "Logs\\");
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

        private static void RegisterControls()
        {
            ControlsService controlsService = AppHost.GetService<ControlsService>();
            controlsService.TryRegisterControl<MultifuntionControl>(MultifuntionControl.ControlGuid);
            controlsService.TryRegisterControl<CursorControl>(CursorControl.ControlGuid);
            controlsService.TryRegisterControl<PenControl>(PenControl.ControlGuid);
        }
    }
}
