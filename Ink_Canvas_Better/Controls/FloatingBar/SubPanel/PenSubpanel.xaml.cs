using System;
using System.Collections.ObjectModel;
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
            Ellipse_Preview.Fill = PenSubpanelSettings.ColorCollection[PenSubpanelSettings.GridViewSelectedIndex];
        }

        public bool TryInvoke()
        {
            if (_isLoaded)
            {
                try
                {
                    var inkCanvasService = App.GetService<InkCanvasService>();
                    var seletedIndex = PenSubpanelSettings.GridViewSelectedIndex;
                    // UI
                    Ellipse_Preview.Fill = PenSubpanelSettings.ColorCollection[seletedIndex];
                    // InkCanvas
                    inkCanvasService.CurrentDrawingAttributes.Color = PenSubpanelSettings.ColorCollection[seletedIndex].Color;
                    inkCanvasService.CurrentDrawingAttributes.Width = inkCanvasService.CurrentDrawingAttributes.Height = Slider_Thickness.Value;
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

        private void GridView_Colors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Toggle_Color.IsChecked == true)
            {
                var seletedIndex = PenSubpanelSettings.GridViewSelectedIndex;
                Popup_ColorPicker.IsOpen = false;
                Popup_ColorPicker.PlacementTarget = GridView_Colors.ItemContainerGenerator.ContainerFromIndex(seletedIndex) as UIElement;
                SqColorPicker.SelectedColor = PenSubpanelSettings.ColorCollection[seletedIndex].Color;
                Popup_ColorPicker.IsOpen = true;
            }
            else if (Popup_ColorPicker.IsOpen == true) Popup_ColorPicker.IsOpen = false;
            this.TryInvoke();
        }

        private void Slider_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.TryInvoke();

        private void Button_Color_Click(object sender, RoutedEventArgs e)
        {
            Popup_ColorPicker.IsOpen = false;
        }

        private void SqColorPicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            var seletedIndex = PenSubpanelSettings.GridViewSelectedIndex;
            PenSubpanelSettings.ColorCollection[seletedIndex].Color = SqColorPicker.SelectedColor;
            this.TryInvoke();
        }

    }

    public class PenSubpanelSettings : INotifyPropertyChanged
    {
        private int _gridViewSelectedIndex = 0;
        private ObservableCollection<SolidColorBrush> _colorCollection =
            [
                ColorConverter.HexToSolidColorBrush("#FFFFFF"),
                ColorConverter.HexToSolidColorBrush("#000000"),
                ColorConverter.HexToSolidColorBrush("#A72C1D"),
                ColorConverter.HexToSolidColorBrush("#E03B27"),
                ColorConverter.HexToSolidColorBrush("#EFC046"),
                ColorConverter.HexToSolidColorBrush("#FCFC58"),
                ColorConverter.HexToSolidColorBrush("#A0CB64"),
                ColorConverter.HexToSolidColorBrush("#59AA5C"),
                ColorConverter.HexToSolidColorBrush("#61ADE9"),
                ColorConverter.HexToSolidColorBrush("#4170B8"),
                ColorConverter.HexToSolidColorBrush("#19275C"),
                ColorConverter.HexToSolidColorBrush("#673C98"),
            ];
        private int _thickness = 1;

        #region

        public int GridViewSelectedIndex
        {
            get { return _gridViewSelectedIndex; }
            set {  _gridViewSelectedIndex = value; OnPropertyChanged(); }
        }

        public ObservableCollection<SolidColorBrush> ColorCollection
        {
            get { return _colorCollection; }
            set { _colorCollection = value; OnPropertyChanged(); }
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
