using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class FloatingBarGroup : ItemsControl
    {
        static FloatingBarGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBarGroup), new FrameworkPropertyMetadata(typeof(FloatingBarGroup)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            ItemsSource = ControlsCollection;
        }
    }
}
