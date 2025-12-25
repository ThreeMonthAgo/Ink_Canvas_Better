using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Newtonsoft.Json;
using static Ink_Canvas_Better.Enums;

namespace Ink_Canvas_Better.Controls.FloatingBar;
public partial class FloatingBar : UserControl, IFloatingBarComponentSettingBase
{
    public static string Guid { get; } = "D4F5C8A1-6E2B-4F3A-9C1E-2B7D8F9A0B1C";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new FloatingBarSettings();

    public FloatingBar()
    {
        InitializeComponent();

        Loaded += FloatingBar_Loaded;
    }

    public bool TryInvoke() => true;

    private void FloatingBar_Loaded(object sender, RoutedEventArgs e)
    {
        (Settings as FloatingBarSettings).IsInitializing = false;
        Dock();
    }

    public FloatingBar Add(IFloatingBarComponentSettingBase component)
    {
        (Settings as FloatingBarSettings).Items.Add(component);
        return this;
    }

    public void Dock(DockPlacement dockPlacement = DockPlacement.Unset)
    {
        // get translateTransform
        var tg = this.RenderTransform as TransformGroup;
        TranslateTransform tt = null;
        ScaleTransform st = null;
        foreach (var item in tg.Children)
        {
            if (item is TranslateTransform translateTransform)
            {
                tt = translateTransform;
                continue;
            }
            if (item is ScaleTransform scaleTransform)
            {
                st = scaleTransform;
                continue;
            }
        }
        if (tt == null) return;
        // dock
        var scWidth = SystemParameters.PrimaryScreenWidth;
        var scHeight = SystemParameters.PrimaryScreenHeight;
        // var wkaWidth = SystemParameters.WorkArea.Width;
        var wkaHeight = SystemParameters.WorkArea.Height;
        var realWidth = this.ActualWidth * st.ScaleX;
        var realHeight = this.ActualHeight * st.ScaleY;
        if (dockPlacement == DockPlacement.Unset)
        {
            dockPlacement = (Settings as FloatingBarSettings).DockPlacement;
        }
        switch (dockPlacement)
        {
            case DockPlacement.Top:
                tt.X = (scWidth / 2) - (realWidth / 2);
                tt.Y = 0;
                break;
            case DockPlacement.Bottom:
                tt.X = (scWidth / 2) - (realWidth / 2);
                tt.Y = scHeight - realHeight;
                break;
            case DockPlacement.AboveTaskBar:
                tt.X = (scWidth / 2) - (realWidth / 2);
                tt.Y = wkaHeight - realHeight;
                break;
            case DockPlacement.Left:
                tt.X = 0;
                tt.Y = (scHeight / 2) - (realHeight / 2);
                break;
            case DockPlacement.Right:
                tt.X = scWidth - realWidth;
                tt.Y = (scHeight / 2) - (realHeight / 2);
                break;
            case DockPlacement.TopLeft:
                tt.X = tt.Y = 0;
                break;
            case DockPlacement.TopRight:
                tt.X = scWidth - realWidth;
                tt.Y = 0;
                break;
            case DockPlacement.AboveTaskBarLeft:
                tt.X = 0;
                tt.Y = wkaHeight - realHeight;
                break;
            case DockPlacement.AboveTaskBarRight:
                tt.X = scWidth - realWidth;
                tt.Y = wkaHeight - realHeight;
                break;
            case DockPlacement.BottomLeft:
                tt.X = scWidth - realWidth;
                tt.Y = scHeight - realHeight;
                break;
            case DockPlacement.BottomRight:
                tt.X = scWidth - realWidth;
                tt.Y = scHeight - realHeight;
                break;
        }
    }
}

public class FloatingBarSettings : INotifyPropertyChanged
{
    private ObservableCollection<IFloatingBarComponentSettingBase>? _items = [];
    private double _spacing = 4.0;
    private Orientation _orientation = Orientation.Horizontal;
    private double _scale = 1.0;
    private int _screenIndex = 0;
    private DockPlacement _dockPlacement = DockPlacement.AboveTaskBar;

    #region

    public ObservableCollection<IFloatingBarComponentSettingBase>? Items
    {
        get { return _items; }
        set { _items = value; OnPropertyChanged(); }
    }

    public double Spacing
    {
        get { return _spacing; }
        set { _spacing = value; OnPropertyChanged(); }
    }

    public Orientation Orientation
    {
        get { return _orientation; }
        set { _orientation = value; OnPropertyChanged(); }
    }

    public double Scale
    {
        get { return _scale; }
        set { _scale = value; OnPropertyChanged(); }
    }

    public int ScreenIndex
    {
        get { return _screenIndex; }
        set { _screenIndex = value; OnPropertyChanged(); }
    }

    public DockPlacement DockPlacement
    {
        get { return _dockPlacement; }
        set { _dockPlacement = value; OnPropertyChanged(); }
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
