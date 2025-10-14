using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class ExPopup
    {
        protected virtual void OnPinButtonClicked(Object s, RoutedEventArgs args)
        {
            StaysOpen = !StaysOpen;
        }

        protected virtual void OnCloseButtonClicked(Object s, RoutedEventArgs args)
        {
            this.IsOpen = false;
        }

        protected override void OnLostMouseCapture(MouseEventArgs e)
        {
            base.OnLostMouseCapture(e);
            e.Handled = false;
        }
    }
}
