using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Ink_Canvas_Better.Services;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;

internal class EraserControlVM : INotifyPropertyChanged
{
    private int _thickness = 20;
    private Visibility _textVisibility = Visibility.Collapsed;

    // ignored below
    private bool _isOpen = false;

    #region

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
