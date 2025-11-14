using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace Ink_Canvas_Better.Controls.Panel
{
    [ContentProperty("Child")]
    public class ExPopup : Popup
    {
        static ExPopup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExPopup), new FrameworkPropertyMetadata(typeof(ExPopup)));
            AllowsTransparencyProperty.OverrideMetadata(typeof(ExPopup), new FrameworkPropertyMetadata(true));
        }
    }
}
