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
            this.TextVisibility = Visibility.Visible;
            this.MouseDown += PenControl_MouseDown;
            this.Loaded += PenControl_Loaded;
        }

        private void PenControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void PenControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Content = new Subpanel()
            {
                Content = new PenSubpanel(),
                PlacementTarget = this
            };
            var Subpanel = this.Content as Subpanel;
            AppHost.GetService<InkCanvasService>().CurrentEditingMode = Enums.EditingMode.Ink;
            Subpanel.IsOpen = true;
            Subpanel.Placement = PlacementMode.Top;
            Subpanel.CaptureMouse();
        }
    }
}
