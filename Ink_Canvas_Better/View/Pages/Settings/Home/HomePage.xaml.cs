using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;

namespace Ink_Canvas_Better.View.Pages.Settings.Home
{
    public partial class HomePage : Page
    {
        private SettingsService settingsService;
        private LanguageWindow languageWindow;

        public HomePage()
        {
            InitializeComponent();

            this.Loaded += Home_Loaded;
        }

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            this.settingsService = IApp.GetService<SettingsService>();
            this.languageWindow = IApp.GetService<LanguageWindow>();
            SettingsCard_About_1.Header = $"Ink Canvas Better v{settingsService.Settings.AppVersion}" + (settingsService.Settings.AppVersion.Revision > 0 ? " - beta" : "");
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            IApp.ShutdownApp();
        }

        private void ButtonRestart_Click(object sender, RoutedEventArgs e)
        {
            IApp.RestartApp();
        }

        private void ButtonLog_Click(object sender, RoutedEventArgs e)
        {
            var p1 = AppDomain.CurrentDomain.BaseDirectory;
            var p2 = settingsService.Settings.LogDirPath;
            var p = Path.Combine(p1, p2);
            if (Directory.Exists(p))
            {
                Process.Start(new ProcessStartInfo($"{p}") { UseShellExecute = true });
            }
        }

        private void ButtonResetSettings_Click(object sender, RoutedEventArgs e)
        {
            settingsService.ResetSettings();
        }

        private void SettingsCard_Github_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/ThreeMonthAgo/Ink_Canvas_Better") { UseShellExecute = true });
        }

        private void SettingsCard_Language_Click(object sender, RoutedEventArgs e)
        {
            languageWindow.ShowDialog();
        }

        private void HyperlinkButton_Author_Click(object sender, RoutedEventArgs e)
        {
            var name = ((iNKORE.UI.WPF.Modern.Controls.HyperlinkButton)sender).Content.ToString();
            Process.Start(new ProcessStartInfo($"https://github.com/{name}") { UseShellExecute = true });
        }

        private void SettingsCard_License_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo($"https://github.com/ThreeMonthAgo/Ink_Canvas_Better/blob/main/LICENSE") { UseShellExecute = true });
        }
    }
}
