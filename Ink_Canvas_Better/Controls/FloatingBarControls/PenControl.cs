using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using Ink_Canvas_Better.Controls.FloatingBarSubpanel;
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
            this.SetResourceReference(TitleProperty, "Text_Pen");
            this.Content = new PenSubpanel();
            this.TextVisibility = Visibility.Visible;
            this.MouseDown += PenControl_MouseDown;
            this.MouseUp += PenControl_MouseUp;
        }

        private void PenControl_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsOpen = !IsOpen;
        }

        private void PenControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // IsOpen = true;
            //AppHost.GetService<InkCanvasService>().CurrentEditingMode = Enums.EditingMode.Ink;
        }
    }
}
