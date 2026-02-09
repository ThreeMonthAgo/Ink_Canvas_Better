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

            IApp.GetService<ComponentService>().DetectAndRegisterComponents();
            this.Startup += App_Startup;
            this.Exit += App_Exit;
        }

        private void App_Exit(object sender, ExitEventArgs e)
        {
            IApp.GetService<MultiscreenService>().Dispose();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            IApp.StartupArgs = e.Args;

            #region log
            this.DispatcherUnhandledException += (sender, e) =>
            {
                IApp.GetService<ILogger<App>>().LogCritical(e.Exception.ToString());
                e.Handled = true;
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                IApp.GetService<ILogger<App>>().LogWarning(e.ToString());
            };
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                IApp.GetService<ILogger<App>>().LogError(e.Exception.ToString());
                e.SetObserved();
            };
            #endregion

            var logger = IApp.GetService<ILogger<App>>();
            Mutex _ = new(true, "Ink_Canvas_Better", out bool ret);
            if (!ret && !IApp.StartupArgs.Contains("-m")) // -m multiple
            {
                logger.LogInformation("Detected existing instance");
                iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(
                    "Another instance of Ink Canvas Better is already running.",
                    "Ink Canvas Better",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                logger.LogInformation("Ink Canvas Batter automatically closed");
                Environment.Exit(0);
            }

            this.MainWindow = IApp.GetService<MainWindow>();
            MainWindow.Show();
            var floatingBarWindow = IApp.GetService<FloatingBarWindow>();
            floatingBarWindow.Owner = IApp.GetService<MainWindow>();
            floatingBarWindow.Show();

            IApp.GetService<SettingsService>().LoadSettings();
            logger.LogInformation($"===== Ink Canvas Better (v{IApp.GetService<SettingsService>().Settings.AppVersion}) is running =====");
            Debug.WriteLine(IApp.GetService<PPTService>());
            Debug.WriteLine(IApp.GetService<MultiscreenService>());
        }

        private void Init()
        {
            IApp.Host = Microsoft.Extensions.Hosting.Host.
                CreateDefaultBuilder().
                ConfigureServices((context, service) =>
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
                    service.AddSingleton<FloatingBarWindow>();
                    service.AddSingleton<SettingsWindow>();
                    service.AddSingleton<LanguageWindow>();

                    // UI (Pages)
                    service.AddSingleton<HomePage>();
                    service.AddSingleton<AppearancePage>();
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
    }
}
