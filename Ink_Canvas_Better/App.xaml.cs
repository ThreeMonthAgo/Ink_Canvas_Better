using System.Collections.Concurrent;
using System.Diagnostics;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Pages.Settings.Appearance;
using Ink_Canvas_Better.Pages.Settings.Home;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better
{
    public partial class App : Application
    {
        private static IHost Host;

        /// <summary>
        /// StartupArgs
        /// </summary>
        /// <remarks>
        /// Args:
        /// <list type="bullet">
        ///     <item>-m multiple</item>
        /// </list>
        /// </remarks>
        public static string[]? StartupArgs { get; set; } = null;

        public App()
        {
            InitializeComponent();
            Init();

            this.MainWindow = GetService<MainWindow>();
            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_OnExit);
            GetService<ComponentService>().RegisterComponents();
            GetService<SettingsService>().LoadSettings();
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            StartupArgs = e.Args;
            #region log
            this.DispatcherUnhandledException += (sender, e) =>
            {
                GetService<ILogger<App>>().LogCritical(e.Exception.ToString());
                e.Handled = true;
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                GetService<ILogger<App>>().LogWarning(e.ToString());
            };
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                GetService<ILogger<App>>().LogError(e.Exception.ToString());
                e.SetObserved();
            };
            #endregion

            Mutex _ = new(true, "Ink_Canvas_Better", out bool ret);
            if (!ret && !StartupArgs.Contains("-m")) // -m multiple
            {
                GetService<ILogger<App>>().LogInformation("Detected existing instance");
                iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(
                    "Another instance of Ink Canvas Better is already running.",
                    "Ink Canvas Better",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                GetService<ILogger<App>>().LogInformation("Ink Canvas Batter automatically closed");
                Environment.Exit(0);
            }

            MainWindow.Show();
            GetService<ILogger<App>>().LogInformation($"===== Ink Canvas Better (v{GetService<SettingsService>().Settings.AppVersion}) is running =====");
        }

        void App_OnExit(object sender, ExitEventArgs e)
        {
            GetService<ILogger<App>>().LogInformation("===== Ink Canvas Better exited =====");
        }

        public static void Init()
        {
            Host = Microsoft.Extensions.Hosting.Host.
                CreateDefaultBuilder().
                ConfigureServices((context, service) =>
                {
                    // Services
                    service.AddSingleton<SettingsService>();
                    service.AddSingleton<ThemeService>();
                    service.AddSingleton<ComponentService>();
                    // UI
                    service.AddSingleton<SettingsWindow>();
                    service.AddSingleton<MainWindow>();
                    service.AddSingleton<LanguageWindow>();
                    // Pages
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

        #region GetService

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

        #endregion
    }
}
