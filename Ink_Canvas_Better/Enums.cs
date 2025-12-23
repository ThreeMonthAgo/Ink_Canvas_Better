
namespace Ink_Canvas_Better
{
    public class Enums
    {
        /// <summary>
        /// Ink Canvas Better editing mode.
        /// Conversion needed before apply it as control inkCanvas's editing mode
        /// </summary>
        public enum EditingMode
        {
            None, // Cursor
            Ink, // Pen
            Select,
            EraseByPoint,
            EraseByStroke,
            Shape
        }

        public enum DockPlacement
        {
            Top,
            Bottom,
            AboveTaskBar,
            Left,
            Right,

            TopLeft,
            TopRight,
            AboveTaskBarLeft,
            AboveTaskBarRight,
            BottomLeft,
            BottomRight,
        }
    }
}
