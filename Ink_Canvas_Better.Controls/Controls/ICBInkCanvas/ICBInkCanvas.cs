using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input.StylusPlugIns;
using Ink_Canvas_Better.Controls.Controls.ICBInkCanvas;

namespace Ink_Canvas_Better.Controls.ICBInkCanvas;

public partial class ICBInkCanvas : InkCanvas
{
    public static StrokeRegistrar StrokeRegistrar { get; } = new();

    public StrokeHistory History { get; set; }

    public StrokeInfo DefaultStrokeInfo
    {
        get { return _defaultStrokeInfo; }
        set
        {
            _defaultStrokeInfo = value;
            StylusPlugIn plugin;
            if (value != null && value.StylusPlugInType != null)
            {
                plugin = Activator.CreateInstance(_defaultStrokeInfo.StylusPlugInType) as StylusPlugIn;
            }
            else
            {
                plugin = null;
            }
            ChangeStylusPlugIn(plugin);
        }
    }

    private StrokeInfo _defaultStrokeInfo = new(typeof(Stroke), typeof(DynamicRenderer));
    private bool _isHistory = false;
    private bool _isClear = false;

    static ICBInkCanvas()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ICBInkCanvas), new FrameworkPropertyMetadata(typeof(ICBInkCanvas)));
    }

    public ICBInkCanvas()
    {
        History = new();
        this.Strokes.StrokesChanged += Strokes_StrokesChanged;
    }

    private void Strokes_StrokesChanged(object sender, StrokeCollectionChangedEventArgs e)
    {
        if (_isHistory) return;
        if (this.EditingMode == InkCanvasEditingMode.EraseByPoint
            || this.EditingMode == InkCanvasEditingMode.EraseByStroke
            || _isClear)
        {
            History.Add(e.Added, e.Removed);
        }
    }

    protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
    {
        try
        {
            var newStroke = Activator.CreateInstance(this.DefaultStrokeInfo.StrokeType, [e.Stroke.StylusPoints, this.DefaultDrawingAttributes.Clone()]) as Stroke;
            History.Add(newStroke);
            SwitchStrokeType(e.Stroke, newStroke);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to create stroke of type {DefaultStrokeInfo.StrokeType}. Exception: {ex}");
        }
    }

    private void SwitchStrokeType(Stroke? prevStroke, Stroke newStroke)
    {
        if (prevStroke != null)
        {
            this.Strokes.Remove(prevStroke);
        }
        this.Strokes.Add(newStroke);
        InkCanvasStrokeCollectedEventArgs args = new(newStroke);
        base.OnStrokeCollected(args);
    }

    public void Redo() => SafetyVar(History.Redo, ref _isHistory);

    public void Undo() => SafetyVar(History.Undo, ref _isHistory);

    public void Clear() => SafetyVar(History.Clear, ref _isClear);

    private void SafetyVar(Action<InkCanvas> f, ref bool b)
    {
        b = !b;
        f(this);
        b = !b;
    }

    /// <remarks>
    /// Currently, only one custom StylusPlugIn is supported, as I
    /// haven't encountered a scenario requiring multiple StylusPlugIns.
    /// If such a need arises in the future, this implementation should
    /// be updated to support multiple StylusPlugIns.
    /// </remarks>
    public void ChangeStylusPlugIn(StylusPlugIn? newPlugIn)
    {
        this.StylusPlugIns.Clear();
        // Ensure that the DynamicRenderer is always the last one. This allows
        // new plug-in to process the stylus input before it is rendered.
        this.StylusPlugIns.Add(newPlugIn);
        this.StylusPlugIns.Add(this.DynamicRenderer);
    }
}
