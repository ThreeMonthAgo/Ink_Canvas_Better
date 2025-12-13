using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Windows
{
    public partial class LanguageWindow : Window
    {
        private readonly SettingsService settingsService;
        private readonly ThemeService themeService;

        public LanguageWindow(SettingsService settingsService, ThemeService themeService)
        {
            this.settingsService = settingsService;
            this.themeService = themeService;

            InitializeComponent();
            LanguageListBox.ItemsSource = new List<String>(themeService.SupportedLanguage.Seconds);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
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
            var value = themeService.SupportedLanguage.GetFirst((String)LanguageListBox.SelectedItem);
            settingsService.Settings.CultureInfo = value ?? themeService.SupportedLanguage.GetFirst(0);
            this.Close();
        }
    }
}
