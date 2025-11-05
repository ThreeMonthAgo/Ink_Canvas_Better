using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    internal class MultifuntionalControl : FloatingBarControlBase, ISerializableControl
    {
        public Guid ControlGuid => new("{03C5FD8D-2880-40F7-BAC5-9D83C347162C}");
        private bool _isMouseDown = false;
        private Point _mouseDownPos, _mouseUpPos, _mouseDownControlPos, _currentMousePos;

        private readonly MainWindow mainWindow = IAppHost.GetService<MainWindow>();
        private readonly FloatingBar floatingBar = IAppHost.GetService<FloatingBar>();

        MultifuntionalControl() {
            Source = (DrawingImage)this.Resources["FUI.Drag"];
            TextVisibility = Visibility.Collapsed;
            Button.MouseDown += Button_MouseDown;
            Button.MouseUp += Button_MouseUp;
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;
            _mouseDownPos = e.GetPosition(mainWindow);
            var transform = floatingBar.RenderTransform as TranslateTransform ?? new TranslateTransform();
            _mouseDownControlPos = new Point(transform.X, transform.Y);
            Button.MouseMove += Button_MouseMove;
            Button.CaptureMouse();
            e.Handled = true;
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                var transform = floatingBar.RenderTransform as TranslateTransform ?? new TranslateTransform();
                _currentMousePos = e.GetPosition(mainWindow);
                transform.X = _mouseDownControlPos.X + _currentMousePos.X - _mouseDownPos.X;
                transform.Y = _mouseDownControlPos.Y + _currentMousePos.Y - _mouseDownPos.Y;
            }
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Button.MouseMove -= Button_MouseMove;
            Button.ReleaseMouseCapture();
            _isMouseDown = false;
            _mouseUpPos = e.GetPosition(mainWindow);
            // TODO: fold the floatingbar
        }

    }
}
