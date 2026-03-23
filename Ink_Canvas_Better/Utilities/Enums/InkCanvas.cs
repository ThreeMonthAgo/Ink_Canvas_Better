namespace Ink_Canvas_Better.Utilities.Enums;

public class InkCanvas
{
    /// <summary>
    /// Ink Canvas Better editing mode.
    /// Conversion needed before apply it as control inkCanvas's editing mode
    /// </summary>
    public enum EditingMode
    {
        /// <summary>Cursor</summary>
        None,
        /// <summary>Pen</summary>
        Ink,
        Select,
        EraseByPoint,
        EraseByStroke,
        Shape
    }

    /// <summary>
    /// Ink Canvas Better eraser mode
    /// Conversion needed before apply it as control inkCanvas's eraser mode
    /// </summary>
    public enum EraserMode
    {
        Stroke,
        SquarePoint,
        EllipsePoint
    }
}
