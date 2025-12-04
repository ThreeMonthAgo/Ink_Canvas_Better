using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using iNKORE.UI.WPF.Modern.Common;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        ILogger logger;
        MainWindow mainWindow;
        SettingsService settingsService;

        public App(ILogger<App> logger, SettingsService settingsService, MainWindow mainWindow)
        {
            this.logger = logger;
            this.settingsService = settingsService;
            this.mainWindow = mainWindow;

            InitializeComponent();
            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_OnExit);
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            #region log
            this.DispatcherUnhandledException += (sender, e) =>
            {
                logger.LogCritical(e.Exception.StackTrace);
                iNKORE.UI.WPF.Modern.Controls.MessageBox.Show(
                    $"* An unexpected error has occurred, which may cause Ink Canvas Better to become unstable." +
                    $"\r\n* It is strongly recommended to save your work and restart the application." +
                    $"\r\n* Please consider reporting this issue at: https://github.com/ThreeMonthAgo/Ink_Canvas_Better." +
                    $"\r\n===== Exception details =====" +
                    $"\r\n{e.Exception}",
                    "Ink Canvas Better",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
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
            if (!ret && !Program.StartupArgs.Contains("-m")) // -m multiple
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
    }
}
