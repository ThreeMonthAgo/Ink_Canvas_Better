using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
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

        public SettingsWindow(HomePage homePage)
        {
            this.homePage = homePage;
            InitializeComponent();
        }

        private void Navigation_SelectionChanged(iUWM.Controls.NavigationView sender, iUWM.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            switch (((iUWM.Controls.NavigationView)sender.SelectedItem).Name)
            {
                case "SettingNaviagtion_Item_Home":
                    SettingsFrame.Navigate(homePage);
                    break;
                case "SettingNaviagtion_Item_StartupAndUpdate":
                    break;
                case "SettingNaviagtion_Item_Appearance":
                    break;
                case "SettingNaviagtion_Item_PPT":
                    break;
                case "SettingNaviagtion_Item_ExperimentalFeatures":
                    break;
            }
        }
    }
}
