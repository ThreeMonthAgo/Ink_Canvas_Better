using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;

namespace Ink_Canvas_Better.View.Windows
{
    public partial class LanguageWindow : Window
    {
        private readonly SettingsService settingsService;

        public LanguageWindow(SettingsService settingsService)
        {
            this.settingsService = settingsService;

            InitializeComponent();
            LanguageListBox.ItemsSource = new List<String>(settingsService.SupportedLanguage.Seconds);
        }

        private void LanguageListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = LanguageListBox.SelectedIndex != -1 ? OK.IsEnabled = true : OK.IsEnabled = false;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            var value = settingsService.SupportedLanguage.GetFirst((String)LanguageListBox.SelectedItem);
            IApp.Settings.CultureInfo = value ?? settingsService.SupportedLanguage.GetFirst(0);
            this.Close();
        }
    }
}
