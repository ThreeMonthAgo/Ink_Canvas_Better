using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Interop;
using System.Windows.Media;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

namespace Ink_Canvas_Better.View.Windows
{
    public partial class MainWindow : Window
    {
        private readonly SolidColorBrush NearlyTransparent = new(Color.FromArgb(1, 255, 255, 255));
        private readonly SolidColorBrush Transparent = Brushes.Transparent;
        public nint handle;

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
                    IsCursorMode(true);
                    break;
                case EditingMode.Ink:
                    inkCanvas.Background = NearlyTransparent;
                    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    IsCursorMode(false);
                    break;
                case EditingMode.EraseByPoint:
                    inkCanvas.Background = NearlyTransparent;
                    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    IsCursorMode(false);
                    break;
                case EditingMode.EraseByStroke:
                    inkCanvas.Background = NearlyTransparent;
                    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
                    IsCursorMode(false);
                    break;
                case EditingMode.Select:
                case EditingMode.Shape:
                    throw new NotImplementedException();
            }

            void IsCursorMode(bool b)
            {
                if (b)
                {
                    DllHelper.AddExtendedStyle((HWND)handle, DllHelper.WS_EX_TRANSPARENT);
                    if (inkCanvas.Strokes.Count == 0)
                    {
                        this.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    DllHelper.RemoveExtendedStyle((HWND)handle, DllHelper.WS_EX_TRANSPARENT);
                    if (this.Visibility != Visibility.Visible)
                    {
                        this.Visibility = Visibility.Visible;
                    }
                }
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
            this.handle = new WindowInteropHelper((Window)sender).Handle;
            DllHelper.SetExtendedStyle((HWND)handle, DllHelper.WS_EX_TOOLWINDOW);
            IApp.GetService<SettingsService>().Settings.MainWindowVM.CurrentEditingMode = EditingMode.None;
        }

        public void RedoStroks() => InkCanvas.Redo();

        public void UndoStrokes() => InkCanvas.Undo();

        public void ClearStrokes() => InkCanvas.Clear();

        public void SaveStrokes() => IApp.GetService<InkCanvasService>().SaveData(InkCanvas);

        public void LoadStrokes(string path) => IApp.GetService<InkCanvasService>().LoadData(InkCanvas, path);

        #endregion
    }
}