using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Highlighter,
            Select,
            EraseByPoint,
            EraseByStroke,
            Shape
        }

    }
}
