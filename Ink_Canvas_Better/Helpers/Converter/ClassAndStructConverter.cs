using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Office.Interop.PowerPoint;
using Windows.Win32.Foundation;

namespace Ink_Canvas_Better.Helpers.Converter;

public static class ClassAndStructConverter
{
    extension(SlideShowWindow w)
    {
        public RECT ToRect() => RECT.FromXYWH((int)w.Left, (int)w.Top, (int)w.Width, (int)w.Height);
    }

    extension(RECT r)
    {
        public static RECT operator *(RECT r1, double s)
        {
            return new RECT((int)(r1.left * s), (int)(r1.top * s), (int)(r1.right * s), (int)(r1.bottom * s));
        }
    }
}
