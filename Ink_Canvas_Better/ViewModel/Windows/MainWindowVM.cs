using System.Collections.ObjectModel;
using System.Windows.Ink;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar;
using Newtonsoft.Json;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

namespace Ink_Canvas_Better.ViewModel.Windows;

public class MainWindowVM : ViewModelBase
{
    private DrawingAttributes _currentDrawingAttributes = new();
    private ObservableCollection<FloatingBarVM> _floatingBarCollection = [ new() ];
    private StylusShape _eraserShape = new EllipseStylusShape(10, 10);

    // ignored below
    private EditingMode _currentEditingMode = EditingMode.None;

    #region

    public DrawingAttributes CurrentDrawingAttributes
    {
        get { return _currentDrawingAttributes; }
        set
        {
            SetProperty(ref _currentDrawingAttributes, value);
            IApp.GetService<MainWindow>().UpdateInkCanvasEditingMode(CurrentEditingMode);
        }
    }

    public ObservableCollection<FloatingBarVM> FloatingBarCollection
    {
        get { return _floatingBarCollection; }
        set { SetProperty(ref _floatingBarCollection, value); }
    }

    public StylusShape EraserShape
    {
        get { return _eraserShape; }
        set
        {
            SetProperty(ref _eraserShape, value);
            IApp.GetService<MainWindow>().UpdateInkCanvasEraserShape(value);
        }
    }

    [JsonIgnore]
    public EditingMode CurrentEditingMode
    {
        get { return _currentEditingMode; }
        set
        {
            SetProperty(ref _currentEditingMode, value);
            IApp.GetService<MainWindow>().UpdateInkCanvasEditingMode(value);
        }
    }

    #endregion
}
