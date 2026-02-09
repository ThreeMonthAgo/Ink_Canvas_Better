using System.ComponentModel;
using System.Windows;
using Ink_Canvas_Better.View.Pages.Settings.Appearance;
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
            this.Loaded += SettingsWindow_Loaded;
        }

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Settings.IsInitializing = false;
        }

        /// <summary>
        /// Hide the window instead of close it in order to avoid InvalidOperationException:
        ///     System.Windows.Window.Show is called on a window that is closing or has been closed.
        /// </summary>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
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

        /// <summary>
        /// Show the SettingsWindow or activate it if miniized
        /// </summary>
        public void ShowWindow()
        {
            if (this.WindowState == WindowState.Minimized) this.WindowState = WindowState.Normal;
            if (!this.IsVisible) this.Show();
            if (!this.IsActive) this.Activate();
        }
    }
}
