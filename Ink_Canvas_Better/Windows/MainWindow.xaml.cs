using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Windows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        SettingsService settingsService;
        public Settings Settings => settingsService.Settings;

        public MainWindow(SettingsService settingsService)
        {
            this.settingsService = settingsService;

            InitializeComponent();
            this.SourceInitialized += Helpers.Win32Helper.MainWindow_SourceInitialized;
        }
    }
}
