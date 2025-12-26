using System;
using System.Windows.Controls;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Pages.Settings.Appearance
{
    public partial class AppearancePage : Page
    {
        private readonly SettingsService settingsService;
        public ViewModels.Settings Settings => settingsService.Settings;

        public AppearancePage(SettingsService settingsService)
        {
            this.settingsService = settingsService;

            InitializeComponent();
        }

        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            settingsService.Settings.Theme = ((ComboBox)sender).SelectedIndex;
        }
    }
}
