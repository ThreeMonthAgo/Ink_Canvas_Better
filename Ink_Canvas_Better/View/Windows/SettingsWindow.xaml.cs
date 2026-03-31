using System.Windows;
using Ink_Canvas_Better.View.Pages.Settings.Appearance;
using Ink_Canvas_Better.View.Pages.Settings.Data;
using Ink_Canvas_Better.View.Pages.Settings.Debug;
using Ink_Canvas_Better.View.Pages.Settings.Home;
using Ink_Canvas_Better.ViewModel.Windows;
using iUWM = iNKORE.UI.WPF.Modern;

namespace Ink_Canvas_Better.View.Windows
{
    public partial class SettingsWindow : Window
    {
        private SettingsWindowVM Settings => DataContext as SettingsWindowVM;

        public SettingsWindow()
        {
            InitializeComponent();

            DataContext = new SettingsWindowVM();
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Settings.IsInitializing = false;
        }

        /// <summary>
        /// Navgate
        /// </summary>
        private void Navigation_SelectionChanged(iUWM.Controls.NavigationView sender, iUWM.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            switch (((iUWM.Controls.NavigationViewItem)sender.SelectedItem).Name)
            {
                case "Home":
                    Settings.SelectedPage = new HomePage();
                    break;
                case "StartupAndUpdate":
                    break;
                case "Data":
                    Settings.SelectedPage = new DataPage();
                    break;
                case "Appearance":
                    Settings.SelectedPage = new AppearancePage();
                    break;
                case "PPT":
                    break;
                case "ExperimentalFeatures":
                    break;
                case "Debug":
                    Settings.SelectedPage = new DebugPage();
                    break;
            }
        }
    }
}
