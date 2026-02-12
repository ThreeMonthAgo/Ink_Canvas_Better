using System.Collections.ObjectModel;
using System.Windows.Controls;
using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.Utilities.DataStructures;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using Newtonsoft.Json;
using static Ink_Canvas_Better.Utilities.Enums.UI;

namespace Ink_Canvas_Better.ViewModel.Controls.FloatingBar;

[Component(
    viewType: typeof(Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBar),
    guid: "D4F5C8A1-6E2B-4F3A-9C1E-2B7D8F9A0B1C")]
public class FloatingBarVM : ViewModelBase
{
    private ObservableCollection<ViewModelBase>? _items = [
        new FloatingBarGroupVM(){
            Items = [
                new CursorControlVM(),
                new PenControlVM(),
                new EraserControlVM(),
                new ClearControlVM(),
                new RetraceControlVM(),
            ]
        },
        new FloatingBarGroupVM(){
            Items = [
                new SettingsControlVM(),
            ]
        },
        ];
    private double _spacing = 4.0;
    private Orientation _orientation = Orientation.Horizontal;
    private double _scale = 1.0;
    private int _screenIndex = 0;
    private DockPlacement _dockPlacement = new() {
        VerticalAlignment = DockVerticalAlignment.AboveTaskBar,
        HorizontalAlignment = DockHorizontalAlignment.Center
    };

    // ignored below
    private double _x = 0;
    private double _y = 0;
    private double _width = 0;
    private double _height = 0;

    #region

    /// <summary>
    /// store viewmodels of items in the floating bar
    /// </summary>
    public ObservableCollection<ViewModelBase>? Items
    {
        get { return _items; }
        set { SetProperty(ref _items, value); }
    }

    public double Spacing
    {
        get { return _spacing; }
        set { SetProperty(ref _spacing, value); }
    }

    public Orientation Orientation
    {
        get { return _orientation; }
        set { SetProperty(ref _orientation, value); }
    }

    public double Scale
    {
        get { return _scale; }
        set { SetProperty(ref _scale, value); }
    }

    /// <remarks>
    /// unused, reserved for multi-monitor support
    /// </remarks>
    public int ScreenIndex
    {
        get { return _screenIndex; }
        set { SetProperty(ref _screenIndex, value); }
    }

    public DockPlacement DockPlacement
    {
        get { return _dockPlacement; }
        set { SetProperty(ref _dockPlacement, value); }
    }

    [JsonIgnore]
    public double X
    {
        get { return _x; }
        set { SetProperty(ref _x, value); }
    }

    [JsonIgnore]
    public double Y
    {
        get { return _y; }
        set { SetProperty(ref _y, value); }
    }

    [JsonIgnore]
    public double Width
    {
        get { return _width; }
        set { SetProperty(ref _width, value); }
    }

    [JsonIgnore]
    public double Height
    {
        get { return _height; }
        set { SetProperty(ref _height, value); }
    }

    #endregion

    public void Dock(DockPlacement? placement = null)
    {
        placement ??= this.DockPlacement;
        // Dock
        switch (placement.VerticalAlignment)
        {
            case DockVerticalAlignment.Top:
                this.Y = 0;
                break;
            case DockVerticalAlignment.Center:
                this.Y = (scHeight() / 2) - (realHeight() / 2);
                break;
            case DockVerticalAlignment.Bottom:
                this.Y = scHeight() - realHeight();
                break;
            case DockVerticalAlignment.AboveTaskBar:
            case DockVerticalAlignment.Unset:
                this.Y = wkaHeight() - realHeight();
                break;
        }
        switch (placement.HorizontalAlignment)
        {
            case DockHorizontalAlignment.Left:
                this.X = 0;
                break;
            case DockHorizontalAlignment.Right:
                this.X = scWidth() - realWidth();
                break;
            case DockHorizontalAlignment.Center:
            case DockHorizontalAlignment.Unset:
                this.X = (scWidth() / 2) - (realWidth() / 2);
                break;
        }

        double scWidth() => DllHelper.Screens[this.ScreenIndex].Width;
        double scHeight() => DllHelper.Screens[this.ScreenIndex].Height;
        //double wkaWidth() => DllHelper.Screens[this.ScreenIndex].WkaWidth;  // never used
        double wkaHeight() => DllHelper.Screens[this.ScreenIndex].WkaHeight;
        double realWidth() => this.Width * this.Scale;
        double realHeight() => this.Height * this.Scale;
    }
}