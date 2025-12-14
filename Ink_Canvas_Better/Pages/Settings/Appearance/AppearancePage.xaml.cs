using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Pages.Settings.Appearance
{
    public partial class AppearancePage : Page
    {
        SettingsService settingsService;
        public Services.Settings Settings => settingsService.Settings;

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
