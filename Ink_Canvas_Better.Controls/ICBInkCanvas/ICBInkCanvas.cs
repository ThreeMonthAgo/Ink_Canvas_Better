using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using Ink_Canvas_Better.Controls.ICBInkCanvas.StrokeType;

namespace Ink_Canvas_Better.Controls.ICBInkCanvas;

public partial class ICBInkCanvas : InkCanvas
{
    public StrokeHistory History { get; }

    static ICBInkCanvas()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ICBInkCanvas), new FrameworkPropertyMetadata(typeof(ICBInkCanvas)));
    }

    public ICBInkCanvas()
    {
        History = new(this);
    }

    protected override void OnStrokesReplaced(InkCanvasStrokesReplacedEventArgs e)
    {
        base.OnStrokesReplaced(e);
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

    public void Redo() => History.Redo();

    public void Undo() => History.Undo();

    public void Clear() => History.Clear();
}
