using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Ink_Canvas_Better.Controls.Helpers;

namespace Ink_Canvas_Better.Controls.Panel
{
    partial class Subpanel
    {
        public virtual void CloseButton_Click(Object sender, RoutedEventArgs args)
        {
            this.IsOpen = false;
            var closeButton = GetTemplateChild("PART_CloseButton") as Button;
            closeButton.Content = new Image() { Source = ThemeHelper.FUI_Dismiss, Height = IconSize, Width = IconSize };
        }

        public virtual void PinToggleButton_Checked(Object sender, RoutedEventArgs args)
        {
            this.StaysOpen = true;
            var pinToggleButton = GetTemplateChild("PART_PinToggleButton") as ToggleButton;
            pinToggleButton.Content = new Image() { Source = ThemeHelper.FUI_PinOff, Height = IconSize, Width = IconSize };
        }

        public virtual void PinToggleButton_Unchecked(Object sender, RoutedEventArgs args)
        {
            this.StaysOpen = false;
            var pinToggleButton = GetTemplateChild("PART_PinToggleButton") as ToggleButton;
            pinToggleButton.Content = new Image() { Source = ThemeHelper.FUI_Pin, Height = IconSize, Width = IconSize };
        }
    }
}
