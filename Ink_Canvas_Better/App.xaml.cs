using System.Configuration;
using System.Data;
using System.Windows;
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
        /// <summary>
        /// StartupArgs:
        /// <list type="bullet">
        /// -m multiple
        /// </list>
        /// </summary>
        public static string[]? StartupArgs { get; set; } = null;
        public static string RootPath { get; } = Environment.GetEnvironmentVariable("APPDATA") + "\\Ink Canvas Better\\";

        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
            this.Exit += new ExitEventHandler(App_OnExit);
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            StartupArgs = e.Args;
            #region log
            AppHost.Init();
            ILogger _logger = AppHost.GetService<ILogger<App>>();
            this.DispatcherUnhandledException += (sender, e) =>
            {
                _logger.LogCritical(e.Exception.StackTrace);
                // TODO: show in the messagebox
                // Ink_Canvas.MainWindow.ShowNewMessage($"抱歉，出现预料之外的异常，可能导致 Ink Canvas 画板运行不稳定。\n建议保存墨迹后重启应用。\n报错信息：\n{e.ToString()}", true);
                e.Handled = true;
            };
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                _logger.LogWarning(e.ToString());
            };
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                _logger.LogError(e.Exception.StackTrace);
                e.SetObserved();
            };
            #endregion
            Mutex _ = new(true, "Ink_Canvas_Better", out bool ret);

            if (!ret && !StartupArgs.Contains("-m")) // -m multiple
            {
                _logger.LogInformation("Detected existing instance");
                MessageBox.Show("Ink Canvas Better is running");
                _logger.LogInformation("Ink Canvas Batter automatically closed");
                Environment.Exit(0);
            }

            AppHost.GetService<SettingsService>().ReadSettings();
            AppHost.GetService<MainWindow>().Show();

            _logger.LogInformation($"===== Ink Canvas Better (v{AppHost.GetService<SettingsService>().Settings.Version}) is running =====");
        }

        void App_OnExit(object sender, ExitEventArgs e)
        {
            ILogger _logger = AppHost.GetService<ILogger<App>>();
            _logger.LogInformation("===== Ink Canvas Better exited =====");
        }
    }
}
