using System.Windows;
using System.Windows.Markup;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class Stackpanel : System.Windows.Controls.Panel
    {
        static Stackpanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Stackpanel), new FrameworkPropertyMetadata(typeof(Stackpanel)));
        }
    }
}
