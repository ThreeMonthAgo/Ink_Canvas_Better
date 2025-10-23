using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class FloatingBar : Control
    {
        static FloatingBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBar), new FrameworkPropertyMetadata(typeof(FloatingBar)));
        }

    }
}
