using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.Basic
{
    public partial class FloatingBarControlBase : ContentControl
    {
        static FloatingBarControlBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(typeof(FloatingBarControlBase)));

            WidthProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(64d));
            HeightProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(64d));
            BorderThicknessProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(new Thickness(2)));
        }
    }
}
