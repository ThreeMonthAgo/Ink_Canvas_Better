using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class FloatingBar : ItemsControl
    {
        static FloatingBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBar), new FrameworkPropertyMetadata(typeof(FloatingBar)));
        }

        public FloatingBar()
        {
            RenderTransform = new TranslateTransform();
        }
    }
}
