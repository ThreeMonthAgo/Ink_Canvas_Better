using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class FloatingBarGroup : ItemsControl
    {
        static FloatingBarGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBarGroup), new FrameworkPropertyMetadata(typeof(FloatingBarGroup)));
        }
    }
}
