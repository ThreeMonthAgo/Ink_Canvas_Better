using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    internal class CursorControl : FloatingBarControlBase, ISerializableControl
    {
        public static Guid ControlGuid => new("{9A703354-E315-4FFE-BB3A-503E0B901DCC}");

        public CursorControl()
        {
            TextBlock.SetResourceReference(TextBlock.TextProperty, "Text_Cursor");
            TextBlock.Visibility = Visibility.Visible;
        }

    }
}
