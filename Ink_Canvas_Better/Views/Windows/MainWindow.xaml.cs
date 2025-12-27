using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Ink_Canvas_Better.ViewModels.Windows;
using static Ink_Canvas_Better.Enums;

namespace Ink_Canvas_Better.Windows
{
    public partial class MainWindow : Window
    {
        private readonly SolidColorBrush NearlyTransparent = new(Color.FromArgb(1, 255, 255, 255));
        private readonly SolidColorBrush Transparent = Brushes.Transparent;

        public MainWindowVM Settings { get; } = new() { IsInitializing = false };

        public MainWindow()
        {
            Settings.PropertyChanged += Settings_PropertyChanged;
            InitializeComponent();
            this.DataContext = Settings;
            this.SourceInitialized += MainWindow_SourceInitialized;
            this.Loaded += MainWindow_Loaded;
        }

        private void Settings_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Settings.CurrentDrawingAttributes):
                    UpdateInkCanvasEditingMode(Settings.CurrentEditingMode);
                    break;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Settings.CurrentEditingMode = Enums.EditingMode.None;
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

        private void UpdateInkCanvasEditingMode(EditingMode mode)
        {
            InkCanvas inkCanvas = MW_InkCanvas;
            switch (mode)
            {
                case EditingMode.None:
                    inkCanvas.Background = Transparent;
                    inkCanvas.EditingMode = InkCanvasEditingMode.None;
                    break;
                case EditingMode.Ink:
                    inkCanvas.Background = NearlyTransparent;
                    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case EditingMode.EraseByPoint:
                    inkCanvas.Background = NearlyTransparent;
                    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    break;
                case EditingMode.EraseByStroke:
                    inkCanvas.Background = NearlyTransparent;
                    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    break;
                case EditingMode.Select:
                case EditingMode.Shape:
                    throw new NotImplementedException();
            }
        }
    }
}
