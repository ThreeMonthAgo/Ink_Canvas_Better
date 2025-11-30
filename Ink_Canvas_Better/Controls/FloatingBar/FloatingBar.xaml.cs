using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;

namespace Ink_Canvas_Better.Controls.FloatingBar
{
    public partial class FloatingBar : FloatingBarComponentBase, IFloatingBarComponentSettingBase
    {
        public object Settings { get; set; } = new FloatingBarSettings();
        public static string Guid { get; } = "D4F5C8A1-6E2B-4F3A-9C1E-2B7D8F9A0B1C";

        public FloatingBar()
        {
            InitializeComponent();

            Loaded += FloatingBar_Loaded;
        }

        private void FloatingBar_Loaded(object sender, RoutedEventArgs e)
        {
            this.RenderTransform = new TranslateTransform();
        }

        public void Add(IFloatingBarComponentSettingBase component)
        {
            ((FloatingBarSettings)Settings).Items.Add(component);
        }

        #region dp

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
        public ObservableCollection<IFloatingBarComponentSettingBase> Items { get; set; } = [ ];
        //public bool IsAutoHideEnabled { get; set; } = false;
        //public int AutoHideDelay { get; set; } = 3; // in seconds
        //public double OpacityWhenHidden { get; set; } = 0.2;
    }
}
