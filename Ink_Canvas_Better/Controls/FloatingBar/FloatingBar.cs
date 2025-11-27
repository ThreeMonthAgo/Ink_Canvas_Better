using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Interface;

namespace Ink_Canvas_Better.Controls.FloatingBar
{
    public class FloatingBar : FloatingBarComponentBase, IFloatingBarComponentSettingBase
    {
        public object Settings { get; set; } = new FloatingBarSettings();
        public string Guid { get; } = "D4F5C8A1-6E2B-4F3A-9C1E-2B7D8F9A0B1C";

        static FloatingBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBar), new FrameworkPropertyMetadata(typeof(FloatingBar)));
        }

        #region dp

        #region ItemsSource

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(object), typeof(FloatingBar), new PropertyMetadata(null));

        #endregion

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
            DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(FloatingBar), new PropertyMetadata(4.0));

        #endregion

        #endregion

    }

    public class FloatingBarSettings
    {
        //public bool IsAutoHideEnabled { get; set; } = false;
        //public int AutoHideDelay { get; set; } = 3; // in seconds
        //public double OpacityWhenHidden { get; set; } = 0.2;
    }
}
