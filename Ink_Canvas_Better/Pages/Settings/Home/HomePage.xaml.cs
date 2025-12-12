using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Pages.Settings.Home
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Page
    {
        private SettingsService settingsService;
        private Language language;

        public HomePage(SettingsService settingsService, Language language)
        {
            this.settingsService = settingsService;
            this.language = language;

            InitializeComponent();

            this.Loaded += Home_Loaded;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonRestart_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Assembly.GetExecutingAssembly().Location, "-m");
            Application.Current.Shutdown();
        }

        private void ButtonLog_Click(object sender, RoutedEventArgs e)
        {
            var p = settingsService.Settings.LogDirPath;
            if (Directory.Exists(p))
            {
                Process.Start(p);
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
            language.ShowDialog();
        }

        private void HyperlinkButton_Author_Click(object sender, RoutedEventArgs e)
        {
            var name = ((iNKORE.UI.WPF.Modern.Controls.HyperlinkButton)sender).Content.ToString();
            Process.Start(new ProcessStartInfo($"https://github.com/{name}") { UseShellExecute = true });
        }

    }
}
