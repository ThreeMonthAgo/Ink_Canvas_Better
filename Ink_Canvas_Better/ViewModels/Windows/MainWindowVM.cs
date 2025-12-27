using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Ink;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar;
using Newtonsoft.Json;
using static Ink_Canvas_Better.Enums;

namespace Ink_Canvas_Better.ViewModels.Windows;

public class MainWindowVM
{
    private DrawingAttributes _currentDrawingAttributes = new();
    private EditingMode _currentEditingMode;
    private ObservableCollection<FloatingBarVM> _floatingBarCollection = [ new FloatingBarVM() ];

    #region

    public DrawingAttributes CurrentDrawingAttributes
    {
        get { return _currentDrawingAttributes; }
        set { SetProperty(ref _currentDrawingAttributes, value); }
    }

    public EditingMode CurrentEditingMode
    {
        get { return _currentEditingMode; }
        set { SetProperty(ref _currentEditingMode, value); }
    }

    public ObservableCollection<FloatingBarVM> FloatingBarCollection
    {
        get { return _floatingBarCollection; }
        set { SetProperty(ref _floatingBarCollection, value); }
    }

    #endregion

    protected virtual void SetProperty<T>(
        ref T field,
        T newValue,
        [CallerMemberName] string? propertyName = null,
        bool force = true)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue))
        {
            if (force) OnPropertyChanged(propertyName);
        }
        else
        {
            field = newValue;
            OnPropertyChanged(propertyName);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null, bool force = true)
    {
        if (!IsInitializing) App.GetService<SettingsService>().SaveSettings();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [JsonIgnore]
    public bool IsInitializing { get; set; } = true;
}
