using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using Ink_Canvas_Better.Controls.ICBInkCanvas.StrokeType;

namespace Ink_Canvas_Better.Controls.ICBInkCanvas;

public class ICBInkCanvas : InkCanvas
{
    static ICBInkCanvas()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ICBInkCanvas), new FrameworkPropertyMetadata(typeof(ICBInkCanvas)));
    }

    protected override void OnStrokeCollected(InkCanvasStrokeCollectedEventArgs e)
    {
        switch (DefaultStrokeType)
        {
            case StrokeType.Default:
                break;
            case StrokeType.TailStroke:
                TailStroke TailStroke = new(e.Stroke.StylusPoints, this.DefaultDrawingAttributes);
                SwitchStrokeType(e.Stroke, TailStroke);
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

    #region Properties

    public StrokeType DefaultStrokeType
    {
        get { return (StrokeType)GetValue(DefaultStrokeTypeProperty); }
        set { SetValue(DefaultStrokeTypeProperty, value); }
    }

    public static readonly DependencyProperty DefaultStrokeTypeProperty =
        DependencyProperty.Register(nameof(DefaultStrokeType), typeof(StrokeType), typeof(ICBInkCanvas), new PropertyMetadata(StrokeType.Default));

    #endregion

    public enum StrokeType
    {
        Default = 0,
        TailStroke = 1,
    }
}
