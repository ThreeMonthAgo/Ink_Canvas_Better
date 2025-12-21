using System.Collections.Concurrent;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using Ink_Canvas_Better.Controls.FloatingBar;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Pages.Settings.Home;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Pages.Settings.Appearance;
using Ink_Canvas_Better.Controls.FloatingBar.SubPanel;

namespace Ink_Canvas_Better
{
    public partial class App : Application
    {
        private readonly ILogger logger;
        private readonly MainWindow mainWindow;
        private readonly SettingsService settingsService;

        public static IHost Host;

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
        public static string RootPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + "\\Ink Canvas Better\\";

        /// <summary>
        /// Used for check
        /// </summary>
        public readonly static ConcurrentDictionary<string, Type> RegisteredControls = new();

        public App()
        {
            InitializeComponent();
            Init();

            this.logger = GetService<ILogger<App>>();
            this.settingsService = GetService<SettingsService>();
            this.mainWindow = GetService<MainWindow>();

            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_OnExit);
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            StartupArgs = e.Args;
            #region log
            this.DispatcherUnhandledException += (sender, e) =>
            {
                logger.LogCritical(e.Exception.ToString());
                e.Handled = true;
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                logger.LogWarning(e.ToString());
            };
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                logger.LogError(e.Exception.ToString());
                e.SetObserved();
            };
            #endregion

            Mutex _ = new(true, "Ink_Canvas_Better", out bool ret);
            if (!ret && !StartupArgs.Contains("-m")) // -m multiple
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

            mainWindow.Show();
            logger.LogInformation($"===== Ink Canvas Better (v{settingsService.Settings.AppVersion}) is running =====");
        }

        void App_OnExit(object sender, ExitEventArgs e)
        {
            logger.LogInformation("===== Ink Canvas Better exited =====");
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
                    // UI
                    service.AddSingleton<MainWindow>();
                    service.AddSingleton<SettingsWindow>();
                    service.AddSingleton<LanguageWindow>();
                    // Pages
                    service.AddSingleton<HomePage>();
                    service.AddSingleton<AppearancePage>();

                    // FloatingBarComponent
                    RegComponents<FloatingBar>(FloatingBar.Guid);
                    RegComponents<FloatingBarGroup>(FloatingBarGroup.Guid);
                    RegComponents<MultifunctionControl>(MultifunctionControl.Guid);
                    RegComponents<SettingsControl>(SettingsControl.Guid);
                    RegComponents<PenControl>(PenControl.Guid);
                    RegComponents<CursorControl>(CursorControl.Guid);

                    // FloatingBarSubpanel
                    RegComponents<PenSubpanel>(PenSubpanel.Guid);

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
            GetService<SettingsService>().LoadSettings();
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
