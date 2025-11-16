using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Controls.Basic;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    internal class PenControl : FloatingBarControlBase, ISerializableControl
    {
        public static Guid ControlGuid => new("{F80A707F-EDB5-420B-9A1F-A2504932C3F4}");

        public PenControl()
        {
            this.SetResourceReference(SourceProperty, "FUI.CalligraphyPen");
            this.SetResourceReference(TextProperty, "Text_Pen");
            this.TextVisibility = Visibility.Visible;
            this.MouseDown += PenControl_MouseDown;
        }

        private void PenControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AppHost.GetService<InkCanvasService>().SwitchInkCanvasMode(InkCanvasEditingMode.Ink);
        }
    }
}
