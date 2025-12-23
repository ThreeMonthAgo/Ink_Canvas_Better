using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Interop;
using System.Windows.Media;
using Ink_Canvas_Better.Services;
using Newtonsoft.Json;
using static Ink_Canvas_Better.Enums;

namespace Ink_Canvas_Better.Windows
{
    public partial class MainWindow : Window
    {
        private readonly SolidColorBrush NearlyTransparent = new(Color.FromArgb(1, 255, 255, 255));
        private readonly SolidColorBrush Transparent = Brushes.Transparent;

        public SettingsService SettingsService { get; }

        public MainWindowSettings Settings { get; } = new() { IsInitializing = false };

        public MainWindow(SettingsService settingsService)
        {
            this.SettingsService = settingsService;
            this.DataContext = this;

            InitializeComponent();
            this.SourceInitialized += MainWindow_SourceInitialized;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.CurrentEditingMode = Enums.EditingMode.None;
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

        #region

        private EditingMode _currentEditingMode;
        public EditingMode CurrentEditingMode
        {
            get => _currentEditingMode;
            set
            {
                if (_currentEditingMode == value) return;
                _currentEditingMode = value;
                InkCanvas inkCanvas = MW_InkCanvas;
                switch (value)
                {
                    case EditingMode.None:
                        inkCanvas.Background = Transparent;
                        inkCanvas.EditingMode = InkCanvasEditingMode.None;
                        break;
                    case EditingMode.Ink:
                        inkCanvas.Background = NearlyTransparent;
                        inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                        break;
                    case EditingMode.Select:
                    case EditingMode.EraseByPoint:
                    case EditingMode.EraseByStroke:
                    case EditingMode.Shape:
                        throw new NotImplementedException();
                }
            }
        }

        #endregion
    }

    public class MainWindowSettings : INotifyPropertyChanged
    {
        private DrawingAttributes _currentDrawingAttributes = new();

        #region

        public DrawingAttributes CurrentDrawingAttributes
        {
            get { return _currentDrawingAttributes; }
            set { _currentDrawingAttributes = value; OnPropertyChanged(); }
        }

        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (!IsInitializing) App.GetService<SettingsService>().SaveSettings();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [JsonIgnore]
        public bool IsInitializing { get; set; } = true;
    }
}
