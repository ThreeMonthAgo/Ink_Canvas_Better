using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Controls.Basic;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    internal class MultifuntionControl : FloatingBarControlBase, ISerializableControl
    {
        public static Guid ControlGuid => new("{03C5FD8D-2880-40F7-BAC5-9D83C347162C}");
        private bool _isMouseDown = false;
        private Point _mouseDownPos, _mouseUpPos, _mouseDownControlPos, _currentMousePos;


        public MultifuntionControl()
        {
            this.SetResourceReference(FloatingBarControlBase.SourceProperty, "FUI.Drag");
            this.TextVisibility = Visibility.Collapsed;

            this.MouseDown += Button_MouseDown;
            this.MouseUp += Button_MouseUp;
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow _mainWindow = IAppHost.GetService<MainWindow>();
            FloatingBar _floatingBar = IAppHost.GetService<FloatingBar>();
            _isMouseDown = true;
            _mouseDownPos = e.GetPosition(_mainWindow);
            var transform = _floatingBar.RenderTransform as TranslateTransform ?? new TranslateTransform();
            _mouseDownControlPos = new Point(transform.X, transform.Y);
            this.MouseMove += Button_MouseMove;
            this.CaptureMouse();
            e.Handled = true;
        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                MainWindow _mainWindow = IAppHost.GetService<MainWindow>();
                FloatingBar _floatingBar = IAppHost.GetService<FloatingBar>();
                var transform = _floatingBar.RenderTransform as TranslateTransform ?? new TranslateTransform();
                _currentMousePos = e.GetPosition(_mainWindow);
                transform.X = _mouseDownControlPos.X + _currentMousePos.X - _mouseDownPos.X;
                transform.Y = _mouseDownControlPos.Y + _currentMousePos.Y - _mouseDownPos.Y;
            }
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow _mainWindow = IAppHost.GetService<MainWindow>();
            this.MouseMove -= Button_MouseMove;
            this.ReleaseMouseCapture();
            _isMouseDown = false;
            _mouseUpPos = e.GetPosition(_mainWindow);
            // TODO: fold the floatingbar
        }

    }
}
