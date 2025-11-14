using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ink_Canvas_Better.Controls.Helpers;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class Subpanel : ContentControl
    {
        const double IconSize = 24d;

        static Subpanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Subpanel), new FrameworkPropertyMetadata(typeof(Subpanel)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var pinToggleButton = GetTemplateChild("PART_PinToggleButton") as ToggleButton;
            pinToggleButton.Checked += PinToggleButton_Checked;
            pinToggleButton.Unchecked += PinToggleButton_Unchecked;
            pinToggleButton.Content = new Image() { Source = StaysOpen ? ThemeHelper.FUI_PinOff : ThemeHelper.FUI_Pin, Height = IconSize, Width = IconSize };
            var closeButton = GetTemplateChild("PART_CloseButton") as Button;
            closeButton.Click += CloseButton_Click;
            closeButton.Content = new Image() { Source = ThemeHelper.FUI_Dismiss, Height = IconSize, Width = IconSize };
        }
    }
}
