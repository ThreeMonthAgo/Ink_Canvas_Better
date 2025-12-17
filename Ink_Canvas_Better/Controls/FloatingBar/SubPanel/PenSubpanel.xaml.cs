using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ColorConverter = Ink_Canvas_Better.Helpers.Converter.ColorConverter;

namespace Ink_Canvas_Better.Controls.FloatingBar.SubPanel
{
    public partial class PenSubpanel : UserControl, IFloatingBarComponentSettingBase
    {
        public static string Guid { get; } = "0683F0B3-9EE7-4E0A-A645-66B157239A03";
        public string ComponentGuid => Guid;
        public object Settings { get; set; } = new PenSubpanelSettings();
        public PenSubpanelSettings PenSubpanelSettings => Settings as PenSubpanelSettings;
        private bool _isLoaded = false;

        public PenSubpanel()
        {
            InitializeComponent();
            DataContext = PenSubpanelSettings;

            Loaded += PenSubpanel_Loaded;
        }

        private void PenSubpanel_Loaded(object sender, RoutedEventArgs e)
        {
            _isLoaded = true;
            PenSubpanelSettings.IsInitializing = false;
            Ellipse_Preview.Fill = ((Rectangle)GridView_Colors.SelectedItem).Fill;
        }

        public bool TryInvoke()
        {
            if (_isLoaded)
            {
                try
                {
                    var mainWindow = App.GetService<MainWindow>();
                    var inkCanvasService = App.GetService<InkCanvasService>();
                    var b = (Rectangle)GridView_Colors.SelectedItem;
                    // UI
                    Ellipse_Preview.Fill = ((Rectangle)GridView_Colors.SelectedItem).Fill;
                    // InkCanvas
                    mainWindow.CurrentDrawingAttributes.Color = ((SolidColorBrush)b.Fill).Color;
                    mainWindow.CurrentDrawingAttributes.Width = mainWindow.CurrentDrawingAttributes.Height = Slider_Thickness.Value;
                    inkCanvasService.CurrentEditingMode = Enums.EditingMode.Ink;
                    return true;
                }
                catch (Exception e)
                {
                    App.GetService<ILogger>().LogWarning(e.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void GridView_Colors_SelectionChanged(object sender, SelectionChangedEventArgs e) => this.TryInvoke();

        private void Slider_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.TryInvoke();
    }

    public class PenSubpanelSettings : INotifyPropertyChanged
    {
        private int _gridViewSelectedIndex = 0;
        private SolidColorBrush _color0 = ColorConverter.HexToSolidColorBrush("#FFFFFF");
        private SolidColorBrush _color1 = ColorConverter.HexToSolidColorBrush("#000000");
        private SolidColorBrush _color2 = ColorConverter.HexToSolidColorBrush("#A72C1D");
        private SolidColorBrush _color3 = ColorConverter.HexToSolidColorBrush("#E03B27");
        private SolidColorBrush _color4 = ColorConverter.HexToSolidColorBrush("#EFC046");
        private SolidColorBrush _color5 = ColorConverter.HexToSolidColorBrush("#FCFC58");
        private SolidColorBrush _color6 = ColorConverter.HexToSolidColorBrush("#A0CB64");
        private SolidColorBrush _color7 = ColorConverter.HexToSolidColorBrush("#59AA5C");
        private SolidColorBrush _color8 = ColorConverter.HexToSolidColorBrush("#61ADE9");
        private SolidColorBrush _color9 = ColorConverter.HexToSolidColorBrush("#4170B8");
        private SolidColorBrush _color10 = ColorConverter.HexToSolidColorBrush("#19275C");
        private SolidColorBrush _color11 = ColorConverter.HexToSolidColorBrush("#673C98");
        private int _thickness = 1;

        #region

        public int GridViewSelectedIndex
        {
            get { return _gridViewSelectedIndex; }
            set {  _gridViewSelectedIndex = value; OnPropertyChanged(); }
        }

        public SolidColorBrush Color0
        {
            get { return _color0; }
            set { _color0 = value; OnPropertyChanged(); }
        } 

        public SolidColorBrush Color1
        {
            get { return _color1; }
            set { _color1 = value; OnPropertyChanged(); }
        } 

        public SolidColorBrush Color2
        {
            get { return _color2; }
            set { _color2 = value; OnPropertyChanged(); }
        } 

        public SolidColorBrush Color3
        {
            get { return _color3; }
            set { _color3 = value; OnPropertyChanged(); }
        } 

        public SolidColorBrush Color4
        {
            get { return _color4; }
            set { _color4 = value; OnPropertyChanged(); }
        } 

        public SolidColorBrush Color5
        {
            get { return _color5; }
            set { _color5 = value; OnPropertyChanged(); }
        } 

        public SolidColorBrush Color6
        {
            get { return _color6; }
            set { _color6 = value; OnPropertyChanged(); }
        } 

        public SolidColorBrush Color7
        {
            get { return _color7; }
            set { _color7 = value; OnPropertyChanged(); }
        } 

        public SolidColorBrush Color8
        {
            get { return _color8; }
            set { _color8 = value; OnPropertyChanged(); }
        } 

        public SolidColorBrush Color9
        {
            get { return _color9; }
            set { _color9 = value; OnPropertyChanged(); }
        } 

        public SolidColorBrush Color10
        {
            get { return _color10; }
            set { _color10 = value; OnPropertyChanged(); }
        }

        public SolidColorBrush Color11
        {
            get { return _color11; }
            set { _color11 = value; OnPropertyChanged(); }
        }

        public int Thickness
        {
            get { return _thickness; }
            set { _thickness = value; OnPropertyChanged(); }
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
