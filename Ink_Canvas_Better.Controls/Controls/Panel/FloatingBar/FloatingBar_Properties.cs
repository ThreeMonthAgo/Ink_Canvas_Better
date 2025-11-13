using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.Panel
{
    partial class FloatingBar
    {
        #region Orientation

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(FloatingBar), new PropertyMetadata(Orientation.Horizontal));

        #endregion
    }
}
