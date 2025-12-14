using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Pages.Settings.Home
{
    public partial class HomePage : Page
    {
        private SettingsService settingsService;
        private LanguageWindow languageWindow;

        public HomePage(SettingsService settingsService, LanguageWindow languageWindow)
        {
            this.settingsService = settingsService;
            this.languageWindow = languageWindow;

            InitializeComponent();

            this.Loaded += Home_Loaded;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonRestart_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Environment.ProcessPath);
            Application.Current.Shutdown();
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

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            SettingsCard_About_1.Header = $"Ink Canvas Better v{settingsService.Settings.AppVersion}" + (settingsService.Settings.AppVersion.Revision > 0 ? " - beta" : "");
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
