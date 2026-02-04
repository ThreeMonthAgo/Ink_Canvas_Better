using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using Ink_Canvas_Better.Controls.ICBInkCanvas.StrokeType;

namespace Ink_Canvas_Better.Controls.ICBInkCanvas;

public partial class ICBInkCanvas : InkCanvas
{
    public StrokeHistory History { get; set; }
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
        if(this.EditingMode == InkCanvasEditingMode.EraseByPoint
            || this.EditingMode == InkCanvasEditingMode.EraseByStroke
            || _isClear)
        {
            History.Add(e.Added, e.Removed);
        }
    }

    protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
    {
        switch (DefaultStrokeType)
        {
            case StrokeType.Default:
                this.DynamicRenderer = new();
                History.Add(e.Stroke);
                break;
            case StrokeType.TailStroke:
                TailStroke tailStroke = new(e.Stroke.StylusPoints, this.DefaultDrawingAttributes);
                History.Add(tailStroke);
                SwitchStrokeType(e.Stroke, tailStroke);
                break;
            case StrokeType.SpeedStroke:
                SpeedStroke speedStroke = new(e.Stroke.StylusPoints, this.DefaultDrawingAttributes);
                History.Add(speedStroke);
                SwitchStrokeType(e.Stroke, speedStroke);
                break;
            default:
                throw new InvalidOperationException($"Unexpected StrokeType: {DefaultStrokeType}");
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
}
