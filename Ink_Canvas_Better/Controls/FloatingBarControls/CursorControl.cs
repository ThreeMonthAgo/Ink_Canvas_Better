using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ink_Canvas_Better.Controls.Basic;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    internal class CursorControl : FloatingBarControlBase, ISerializableControl
    {
        public static Guid ControlGuid => new("{9A703354-E315-4FFE-BB3A-503E0B901DCC}");


        public CursorControl()
        {
            this.SetResourceReference(SourceProperty, "FUI.Cursor");
            this.SetResourceReference(TextProperty, "Text_Cursor");
            this.TextVisibility = Visibility.Visible;
            this.MouseDown += CursorControl_MouseDown;
        }

        private void CursorControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AppHost.GetService<InkCanvasService>().SwitchInkCanvasMode(InkCanvasEditingMode.None);
        }
    }
}
