using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    [ContentProperty("PopupContent")]
    public partial class FloatingBarPopup : Control
    {
        static FloatingBarPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBarPopup), new FrameworkPropertyMetadata(typeof(FloatingBarPopup)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var grid = GetTemplateChild("PART_Grid") as Grid;
            grid.MouseUp += (s, e) => RaiseEvent(new RoutedEventArgs(ClickEvent));

            var popup = GetTemplateChild("PART_Popup") as Popup;
            var mainGrid = GetTemplateChild("PART_MainGrid") as Grid;
            popup.SetBinding(
                Popup.IsOpenProperty,
                new Binding("IsOpen")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                });
            popup.PlacementTarget = mainGrid;

            var toggleButton = GetTemplateChild("PART_ToggleButton") as ToggleButton;
            toggleButton.SetBinding(
                ToggleButton.IsCheckedProperty,
                new Binding("StaysOpen")
                {
                    Source = this,
                    Mode = BindingMode.TwoWay
                });
        }
    }
}
