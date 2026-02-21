using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    public partial class FloatingBarButton : Control
    {
        static FloatingBarButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBarButton), new FrameworkPropertyMetadata(typeof(FloatingBarButton)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var grid = GetTemplateChild("PART_MainGrid") as Grid;
            grid.MouseUp += (s, e) => RaiseEvent(new RoutedEventArgs(ClickEvent));
        }
    }
}
