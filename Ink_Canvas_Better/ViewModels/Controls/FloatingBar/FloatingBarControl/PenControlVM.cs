using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using Ink_Canvas_Better.Services;
using Newtonsoft.Json;
using ColorConverter = Ink_Canvas_Better.Helpers.Converter.ColorConverter;

namespace Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;

internal class PenControlVM : INotifyPropertyChanged
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

    // ignored below
    private bool _isOpen = false;
    private SolidColorBrush _ellipseFill;
    private Visibility _textVisibility = Visibility.Collapsed;

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


    [JsonIgnore]
    public bool IsOpen
    {
        get { return _isOpen; }
        set { _isOpen = value; OnPropertyChanged(); }
    }

    [JsonIgnore]
    public SolidColorBrush EllipseFill
    {
        get { return _ellipseFill; }
        set { _ellipseFill = value; OnPropertyChanged(); }
    }

    [JsonIgnore]
    public Visibility TextVisibility
    {
        get { return _textVisibility; }
        set { _textVisibility = value; OnPropertyChanged(); }
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
