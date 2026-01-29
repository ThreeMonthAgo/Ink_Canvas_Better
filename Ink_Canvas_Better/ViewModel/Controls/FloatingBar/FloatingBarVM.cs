using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.Utilities.DataStructures;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
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
    private int _screenIndex = 0; // unused, reserved for multi-monitor support
    private DockPlacement _dockPlacement = new() {
        VerticalAlignment = DockVerticalAlignment.AboveTaskBar,
        HorizontalAlignment = DockHorizontalAlignment.Center
    };

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

    #endregion
}