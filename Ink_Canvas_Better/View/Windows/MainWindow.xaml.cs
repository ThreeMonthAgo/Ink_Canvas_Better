using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.ViewModel.Windows;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

namespace Ink_Canvas_Better.View.Windows
{
    public partial class MainWindow : Window
    {
        private readonly SolidColorBrush NearlyTransparent = new(Color.FromArgb(1, 255, 255, 255));
        private readonly SolidColorBrush Transparent = Brushes.Transparent;

        public MainWindowVM Settings => DataContext as MainWindowVM;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowVM();
            Settings.PropertyChanged += Settings_PropertyChanged;
            this.Loaded += MainWindow_Loaded;
        }

        private void Settings_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Settings.CurrentEditingMode):
                case nameof(Settings.CurrentDrawingAttributes):
                    UpdateInkCanvasEditingMode(Settings.CurrentEditingMode);
                    break;
            }
        }

        private void UpdateInkCanvasEditingMode(EditingMode mode)
        {
            InkCanvas inkCanvas = InkCanvas;
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

        #region

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var handle = new WindowInteropHelper((Window)sender).Handle;
            int extendedStyle = Win32Helper.GetWindowLong(handle, Win32Helper.GWL_EXSTYLE);
            _ = Win32Helper.SetWindowLong(
                handle,
                Win32Helper.GWL_EXSTYLE,
                extendedStyle | Win32Helper.WS_EX_TOOLWINDOW
            );
        }

        #endregion
    }
}