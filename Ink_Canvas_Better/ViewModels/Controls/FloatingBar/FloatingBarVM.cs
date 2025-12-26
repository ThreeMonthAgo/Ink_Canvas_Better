using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Newtonsoft.Json;
using static Ink_Canvas_Better.Enums;

namespace Ink_Canvas_Better.ViewModels.Controls.FloatingBar;

internal class FloatingBarVM : INotifyPropertyChanged
{
    private ObservableCollection<IFloatingBarComponentSettingBase>? _items = [];
    private double _spacing = 4.0;
    private Orientation _orientation = Orientation.Horizontal;
    private double _scale = 1.0;
    private int _screenIndex = 0; // unused, reserved for multi-monitor support
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