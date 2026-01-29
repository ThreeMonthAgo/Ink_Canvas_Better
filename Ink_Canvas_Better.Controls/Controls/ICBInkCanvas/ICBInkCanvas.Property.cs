using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Ink_Canvas_Better.Controls.ICBInkCanvas;

public partial class ICBInkCanvas
{
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
        Default,
        TailStroke,
        SpeedStroke,
    }
}
