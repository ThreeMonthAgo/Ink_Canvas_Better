using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.DataStructures;
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
        (Settings as FloatingBarSettings).PropertyChanged += FloatingBar_PropertyChanged;
    }

    private void FloatingBar_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "ScreenIndex":

            default:
                break;
        }
    }

    public bool TryInvoke() => true;

    private void FloatingBar_Loaded(object sender, RoutedEventArgs e)
    {
        (Settings as FloatingBarSettings).IsInitializing = false;
        Dock(DockPlacement.AboveTaskBar);
    }

    public FloatingBar Add(IFloatingBarComponentSettingBase component)
    {
        (Settings as FloatingBarSettings).Items.Add(component);
        return this;
    }

    public void Dock(DockPlacement dockPlacement)
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
        var multiscreenService =  App.GetService<MultiscreenService>();
        Screen screen = multiscreenService.GetScreen((Settings as FloatingBarSettings).ScreenIndex);
        switch (dockPlacement)
        { // TODO
            case DockPlacement.Top:
                tt.X = screen.X + (screen.Width / 2) - ((this.ActualWidth * st.ScaleX) / 2);
                tt.Y = 0;
                break;
            case DockPlacement.Bottom:
                tt.X = screen.X + (screen.Width / 2) - ((this.ActualWidth * st.ScaleX) / 2);
                tt.Y = screen.Height - (this.ActualHeight * st.ScaleX);
                break;
            case DockPlacement.AboveTaskBar:
                tt.X = screen.X + (screen.Width / 2) - ((this.ActualWidth * st.ScaleX) / 2);
                tt.Y = screen.Y + SystemParameters.WorkArea.Height - (this.ActualHeight * st.ScaleX);
                break;
            case DockPlacement.Left:
                break;
            case DockPlacement.Right:
                break;
            case DockPlacement.TopLeft:
                break;
            case DockPlacement.TopRight:
                break;
            case DockPlacement.AboveTaskBarLeft:
                break;
            case DockPlacement.AboveTaskBarRight:
                break;
            case DockPlacement.BottomLeft:
                break;
            case DockPlacement.BottomRight:
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
