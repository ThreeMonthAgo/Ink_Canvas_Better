using System.Configuration;
using System.Data;
using System.Windows;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
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
            InitializeComponent();
            this.logger = logger;
            this.settingsService = settingsService;
            this.mainWindow = mainWindow;

            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_OnExit);
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            #region log
            this.DispatcherUnhandledException += (sender, e) =>
            {
                logger.LogCritical(e.Exception.StackTrace);
                // TODO: show in the messagebox
                // Ink_Canvas.MainWindow.ShowNewMessage($"抱歉，出现预料之外的异常，可能导致 Ink Canvas 画板运行不稳定。\n建议保存墨迹后重启应用。\n报错信息：\n{e.ToString()}", true);
                e.Handled = true;
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                logger.LogWarning(e.ToString());
            };
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                logger.LogError(e.Exception.StackTrace);
                e.SetObserved();
            };
            #endregion
            Mutex _ = new(true, "Ink_Canvas_Better", out bool ret);

            //if (!ret && !StartupArgs.Contains("-m")) // -m multiple
            //{
            //    logger.LogInformation("Detected existing instance");
            //    MessageBox.Show("Ink Canvas Better is running");
            //    logger.LogInformation("Ink Canvas Batter automatically closed");
            //    Environment.Exit(0);
            //}

            //IAppHost.GetService<SettingsService>().ReadSettings();
            mainWindow.Show();

            logger.LogInformation($"===== Ink Canvas Better (v{settingsService.Settings.Version}) is running =====");
        }

        void App_OnExit(object sender, ExitEventArgs e)
        {
            logger.LogInformation("===== Ink Canvas Better exited =====");
        }
    }
}
