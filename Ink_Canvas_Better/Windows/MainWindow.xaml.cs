using System;
using System.Text;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Interop;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Windows
{
    public partial class MainWindow : Window
    {
        private SettingsService settingsService;
        private InkCanvasService inkCanvasService;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.SourceInitialized += MainWindow_SourceInitialized;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.settingsService = App.GetService<SettingsService>();
            this.inkCanvasService = App.GetService<InkCanvasService>();
        }

        private void MainWindow_SourceInitialized(object? sender, EventArgs e)
        {
            var handle = new WindowInteropHelper((Window)sender).Handle;
            int extendedStyle = Helpers.Win32Helper.GetWindowLong(handle, Helpers.Win32Helper.GWL_EXSTYLE);
            _ = Helpers.Win32Helper.SetWindowLong(
                handle,
                Helpers.Win32Helper.GWL_EXSTYLE,
                extendedStyle | Helpers.Win32Helper.WS_EX_TOOLWINDOW
            );
        }
    }
}
