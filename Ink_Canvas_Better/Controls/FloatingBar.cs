using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ink_Canvas_Better.Controls
{
    public class FloatingBar : ItemsControl
    {
        static FloatingBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBar), new FrameworkPropertyMetadata(typeof(FloatingBar)));
        }

        public FloatingBar()
        {
            RenderTransform = new TranslateTransform();
        }

        #region Properties

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

        #endregion

        public void Add(FloatingBarGroup fg)
        {
            this.Items.Add(fg);
        }

        public void Clear()
        {
            this.Items.Clear();
        }
    }
}
