using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Newtonsoft.Json;
using ColorConverter = Ink_Canvas_Better.Helpers.Converter.ColorConverter;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
public partial class PenControl : UserControl, IFloatingBarComponentSettingBase
{
    private MainWindow mainWindow;

    public static string Guid { get; } = "87F7581C-364A-49D7-93C3-3355A8415D38";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new PenControlSettings();

    public PenControl()
    {
        InitializeComponent();

        DataContext = Settings;
        this.Loaded += PenControl_Loaded;
        this.MouseUp += PenControl_MouseUp;
    }

    private void PenControl_Loaded(object sender, RoutedEventArgs e)
    {
        mainWindow = App.GetService<MainWindow>();
        (Settings as PenControlSettings).IsInitializing = false;
        (Settings as PenControlSettings).EllipseFill = (Settings as PenControlSettings).ColorCollection[(Settings as PenControlSettings).GridViewSelectedIndex];
    }

    private void PenControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (mainWindow.CurrentEditingMode != Enums.EditingMode.Ink)
        {
            mainWindow.CurrentEditingMode = Enums.EditingMode.Ink;
        }
        else
        {
            (Settings as PenControlSettings).IsOpen = true;
        }
        this.TryInvoke();
    }

    public bool TryInvoke()
    {
        if ((Settings as PenControlSettings).IsInitializing) return false;
        try
        {
            var mainWindow = App.GetService<MainWindow>();
            var seletedIndex = (Settings as PenControlSettings).GridViewSelectedIndex;
            // UI
            (Settings as PenControlSettings).EllipseFill = (Settings as PenControlSettings).ColorCollection[seletedIndex];
            // InkCanvas
            mainWindow.Settings.CurrentDrawingAttributes.Color = (Settings as PenControlSettings).ColorCollection[seletedIndex].Color;
            mainWindow.Settings.CurrentDrawingAttributes.Width = mainWindow.Settings.CurrentDrawingAttributes.Height = Slider_Thickness.Value;
            mainWindow.CurrentEditingMode = Enums.EditingMode.Ink;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void GridView_Colors_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Toggle_Color.IsChecked == true)
        {
            var seletedIndex = (Settings as PenControlSettings).GridViewSelectedIndex;
            Popup_ColorPicker.IsOpen = false;
            Popup_ColorPicker.PlacementTarget = GridView_Colors.ItemContainerGenerator.ContainerFromIndex(seletedIndex) as UIElement;
            SqColorPicker.SelectedColor = (Settings as PenControlSettings).ColorCollection[seletedIndex].Color;
            Popup_ColorPicker.IsOpen = true;
        }
        else if (Popup_ColorPicker.IsOpen == true) Popup_ColorPicker.IsOpen = false;
        this.TryInvoke();
    }

    private void Slider_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.TryInvoke();

    private void SqColorPicker_ColorChanged(object sender, RoutedEventArgs e)
    {
        var seletedIndex = (Settings as PenControlSettings).GridViewSelectedIndex;
        (Settings as PenControlSettings).ColorCollection[seletedIndex].Color = SqColorPicker.SelectedColor;
        this.TryInvoke();
    }
}

public class PenControlSettings : INotifyPropertyChanged
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
        set { _gridViewSelectedIndex = value; OnPropertyChanged(); }
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

    [JsonIgnore]
    public bool IsOpen { get; set; } = false;

    [JsonIgnore]
    public bool StaysOpen { get; set; } = false;

    [JsonIgnore]
    public PopupAnimation PopupAnimation { get; set; } = PopupAnimation.Fade;

    [JsonIgnore]
    public SolidColorBrush EllipseFill { get; set; }

    [JsonIgnore]
    public Visibility TextVisibility { get; set; } = Visibility.Collapsed;
}

