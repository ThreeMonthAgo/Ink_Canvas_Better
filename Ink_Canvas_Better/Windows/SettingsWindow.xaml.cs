using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using Ink_Canvas_Better.Pages.Settings.Appearance;
using Ink_Canvas_Better.Pages.Settings.Home;
using iUWM = iNKORE.UI.WPF.Modern;

namespace Ink_Canvas_Better.Windows
{
    /// <summary>
    /// SettingsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private HomePage homePage;
        private AppearancePage appearancePage;

        public SettingsWindow(HomePage homePage, AppearancePage appearancePage)
        {
            this.homePage = homePage;
            this.appearancePage = appearancePage;

            InitializeComponent();

            this.Loaded += SettingsWindow_Loaded;
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

        private void SettingsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SettingNaviagtion_Item_Home.IsSelected = true;
        }

        /// <summary>
        /// Navgate
        /// </summary>
        private void Navigation_SelectionChanged(iUWM.Controls.NavigationView sender, iUWM.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            switch (((iUWM.Controls.NavigationViewItem)sender.SelectedItem).Name)
            {
                case "SettingNaviagtion_Item_Home":
                    SettingsFrame.Navigate(homePage);
                    break;
                case "SettingNaviagtion_Item_StartupAndUpdate":
                    break;
                case "SettingNaviagtion_Item_Appearance":
                    SettingsFrame.Navigate(appearancePage);
                    break;
                case "SettingNaviagtion_Item_PPT":
                    break;
                case "SettingNaviagtion_Item_ExperimentalFeatures":
                    break;
            }
        }

        /// <summary>
        /// Show the SettingsWindow or activate it if miniized
        /// </summary>
        public void ShowSettingsWindow()
        {
            if (this.WindowState == WindowState.Minimized) this.WindowState = WindowState.Normal;
            if (!this.IsVisible) this.Show();
            if (!this.IsActive) this.Activate();
        }
    }
}
