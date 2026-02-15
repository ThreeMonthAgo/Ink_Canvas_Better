using System.Diagnostics;
using System.Windows;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Pages.Settings.Appearance;
using Ink_Canvas_Better.View.Pages.Settings.Home;
using Ink_Canvas_Better.View.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IApp
    {
        public App()
        {
            InitializeComponent();
            Init();
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            IApp.GetService<MultiscreenService>().Dispose();
            IApp.GetService<ILogger<App>>().WriteLog(LogLevel.Information, () => $"===== Ink Canvas Better (v{IApp.GetService<SettingsService>().Settings.AppVersion}) terminated =====");
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            IApp.StartupArgs = e.Args;

            var logger = IApp.GetService<ILogger<App>>();
            Mutex _ = new(true, "Ink_Canvas_Better", out bool ret);
            if (!ret && !IApp.StartupArgs.Contains("-m")) // -m multiple
            {
                logger.WriteLog(LogLevel.Information, "Detected existing instance");
                iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(
                    "Another instance of Ink Canvas Better is already running.",
                    "Ink Canvas Better",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                logger.WriteLog(LogLevel.Information, "Ink Canvas Batter automatically closed");
                Environment.Exit(0);
            }
            logger.WriteLog(LogLevel.Information, () => $"===== Ink Canvas Better (v{IApp.GetService<SettingsService>().Settings.AppVersion}) is starting up =====");

            #region log
            this.DispatcherUnhandledException += (sender, e) =>
            {
                IApp.GetService<ILogger<App>>().WriteLog(LogLevel.Critical, e.Exception.ToString);
                e.Handled = true;
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                IApp.GetService<ILogger<App>>().WriteLog(LogLevel.Warning, e.ToString);
            };
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                IApp.GetService<ILogger<App>>().WriteLog(LogLevel.Error, e.Exception, e.ToString);
                e.SetObserved();
            };
            #endregion

            IApp.GetService<ComponentService>().DetectAndRegisterComponents();
            IApp.GetService<SettingsService>().LoadSettings();
            this.MainWindow = IApp.GetService<MainWindow>();
            MainWindow.Show();
            IApp.GetService<MultiscreenService>().Check();
            IApp.GetService<PPTService>().Init();

            logger.WriteLog(LogLevel.Information, () => $"===== Ink Canvas Better (v{IApp.GetService<SettingsService>().Settings.AppVersion}) is running =====");
        }

        private void Init()
        {
            IApp.Host = Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder()
                .ConfigureServices((context, service) =>
                {
                    // Services
                    service.AddSingleton<ComponentService>();
                    service.AddSingleton<SettingsService>();
                    service.AddSingleton<ThemeService>();
                    service.AddSingleton<InkCanvasService>();
                    service.AddSingleton<PPTService>();
                    service.AddSingleton<MultiscreenService>();

                    // UI (Windows)
                    service.AddSingleton<MainWindow>();
                    service.AddSingleton<SettingsWindow>();
                    service.AddSingleton<LanguageWindow>();

                    // UI (Pages)
                    service.AddSingleton<HomePage>();
                    service.AddSingleton<AppearancePage>();
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.ClearProviders();
#if DEBUG
                    logging.SetMinimumLevel(LogLevel.Trace);
                    logging.AddDebugLogger((config) =>
                    {
                        config.MinimumLogLevel = LogLevel.Trace;
                    });
#endif
                    logging.AddFileLogger((config) =>
                    {
                        config.MinimumLogLevel = LogLevel.Trace;
                    });
                })
                .Build();
        }
    }
}
