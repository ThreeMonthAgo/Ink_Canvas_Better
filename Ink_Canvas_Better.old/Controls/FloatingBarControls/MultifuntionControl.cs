using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Interfaces.FloatingBar;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    internal class MultifuntionControl : Control, IFloatingBarControlSettingBase
    {
        public static Guid ControlGuid => new("{03C5FD8D-2880-40F7-BAC5-9D83C347162C}");

        public object Settings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private bool _isMouseDown = false;
        private Point _mouseDownPos, _mouseUpPos, _mouseDownControlPos, _currentMousePos;


        public MultifuntionControl()
        {
            //this.SetResourceReference(SourceProperty, "FUI.Drag");
            //this.TextVisibility = Visibility.Collapsed;

            this.MouseDown += MultifuntionControl_MouseDown;
            this.MouseUp += MultifuntionControl_MouseUp;
        }

        private void MultifuntionControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FloatingBar _floatingBar = AppHost.GetService<FloatingBar>();
            _isMouseDown = true;
            _mouseDownPos = e.GetPosition(Application.Current.MainWindow);
            var transform = _floatingBar.RenderTransform as TranslateTransform ?? new TranslateTransform();
            _mouseDownControlPos = new Point(transform.X, transform.Y);
            this.MouseMove += MultifuntionControl_MouseMove;
            this.CaptureMouse();
            e.Handled = true;
        }

        private void MultifuntionControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                FloatingBar _floatingBar = AppHost.GetService<FloatingBar>();
                TranslateTransform transform = (TranslateTransform)_floatingBar.RenderTransform;
                _currentMousePos = e.GetPosition(Application.Current.MainWindow);
                transform.X = _mouseDownControlPos.X + _currentMousePos.X - _mouseDownPos.X;
                transform.Y = _mouseDownControlPos.Y + _currentMousePos.Y - _mouseDownPos.Y;
            }
        }

        private void MultifuntionControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.MouseMove -= MultifuntionControl_MouseMove;
            this.ReleaseMouseCapture();
            _isMouseDown = false;
            _mouseUpPos = e.GetPosition(Application.Current.MainWindow);
            // TODO: fold the floatingbar
        }
    }
}
