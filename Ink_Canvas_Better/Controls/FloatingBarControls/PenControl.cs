using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Ink_Canvas_Better.Controls.Basic;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    internal class PenControl : FloatingBarControlBase, ISerializableControl
    {
        public static Guid ControlGuid => new("{F80A707F-EDB5-420B-9A1F-A2504932C3F4}");

        public PenControl()
        {
            this.SetResourceReference(SourceProperty, "FUI.CalligraphyPen");
            this.SetResourceReference(TextProperty, "Text_Pen");
            this.TextVisibility = Visibility.Visible;
        }
    }
}
