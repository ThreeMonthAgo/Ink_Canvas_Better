using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Windows;
using Microsoft.Xaml.Behaviors;

namespace Ink_Canvas_Better.Behaviors
{
    // TODO: Support more control
    public class DragBehavior : Behavior<FrameworkElement>
    {
        private Popup _controlToDrag;
        private bool _isDragging;
        private Point _lastMousePosition;

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(DragBehavior), new PropertyMetadata(true));

        protected override void OnAttached()
        {
            base.OnAttached();

            if (IsEnabled)
            {
                AssociatedObject.MouseDown += OnMouseDown;
                AssociatedObject.MouseUp += OnMouseMove;
                AssociatedObject.MouseUp += OnMouseUp;
                AssociatedObject.LostMouseCapture += OnLostMouseCapture;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseDown -= OnMouseDown;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.LostMouseCapture -= OnLostMouseCapture;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var t = Helpers.VisualTreeHelper.GetLogicalParent<Popup>(AssociatedObject);
            if (t != null)
            {
                _controlToDrag = t;
            }
            else return;

            _isDragging = true;
            _lastMousePosition = e.GetPosition(null);

            AssociatedObject.CaptureMouse();
            e.Handled = true;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging || _controlToDrag == null) return;

            Point currentPosition = e.GetPosition(null);

            double deltaX = currentPosition.X - _lastMousePosition.X;
            double deltaY = currentPosition.Y - _lastMousePosition.Y;
            
            _controlToDrag.HorizontalOffset += deltaX;
            _controlToDrag.VerticalOffset += deltaY;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            EndDrag();
        }

        private void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
            EndDrag();
        }

        private void EndDrag()
        {
            _isDragging = false;
            AssociatedObject.ReleaseMouseCapture();
        }
    }
}
