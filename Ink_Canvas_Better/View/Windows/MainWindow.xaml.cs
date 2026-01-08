using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Interop;
using System.Windows.Media;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

namespace Ink_Canvas_Better.View.Windows
{
    public partial class MainWindow : Window
    {
        private readonly SolidColorBrush NearlyTransparent = new(Color.FromArgb(1, 255, 255, 255));
        private readonly SolidColorBrush Transparent = Brushes.Transparent;

        /// <summary>
        /// Avoid operating MainWindow directly, use
        /// <strong> IApp.GetService -> SettingsService -> Settings -> MainWindowVM </strong>
        /// instead.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        public void UpdateInkCanvasEditingMode(EditingMode mode)
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

        public void UpdateInkCanvasEraserShape(StylusShape shape)
        {
            InkCanvas inkCanvas = InkCanvas;
            inkCanvas.EraserShape = shape;
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
            IApp.GetService<SettingsService>().Settings.MainWindowVM.CurrentEditingMode = EditingMode.None;
        }

        #endregion
    }
}