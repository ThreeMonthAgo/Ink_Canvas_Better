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
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(FloatingBar), new PropertyMetadata(Orientation.Horizontal));

        #endregion

        #region Spacing

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(FloatingBar), new PropertyMetadata(4d));

        #endregion
    }
}
