using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Ink;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using Newtonsoft.Json;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

namespace Ink_Canvas_Better.ViewModel.Windows;

public class MainWindowVM
{
    private DrawingAttributes _currentDrawingAttributes = new();
    private EditingMode _currentEditingMode;
    private ObservableCollection<FloatingBarVM> _floatingBarCollection = [ new() ];
    private StylusShape _eraserShape = new EllipseStylusShape(10, 10);

    #region

    public DrawingAttributes CurrentDrawingAttributes
    {
        get { return _currentDrawingAttributes; }
        set { SetProperty(ref _currentDrawingAttributes, value, () =>
        {
            IApp.GetService<MainWindow>().UpdateInkCanvasEditingMode(CurrentEditingMode);
        }); }
    }

    [JsonIgnore]
    public EditingMode CurrentEditingMode
    {
        get { return _currentEditingMode; }
        set { SetProperty(ref _currentEditingMode, value, () =>
        {
            IApp.GetService<MainWindow>().UpdateInkCanvasEditingMode(value);
        }); }
    }

    public ObservableCollection<FloatingBarVM> FloatingBarCollection
    {
        get { return _floatingBarCollection; }
        set { SetProperty(ref _floatingBarCollection, value); }
    }

    public StylusShape EraserShape
    {
        get { return _eraserShape; }
        set { SetProperty(ref _eraserShape, value, () =>
        {
            IApp.GetService<MainWindow>().UpdateInkCanvasEraserShape(value);
        }); }
    }

    #endregion

    protected virtual void SetProperty<T>(
        ref T field,
        T newValue,
        Action? onChanged = null,
        bool force = true,
        [CallerMemberName] string? propertyName = null)
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
        onChanged?.Invoke();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null, bool force = true)
    {
        if (!IsInitializing) IApp.GetService<SettingsService>().SaveSettings();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [JsonIgnore]
    public bool IsInitializing { get; set; } = true;
}
