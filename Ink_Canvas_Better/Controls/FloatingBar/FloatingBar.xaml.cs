using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Controls.FloatingBar
{
    public partial class FloatingBar : UserControl, IFloatingBarComponentSettingBase
    {
        public static string Guid { get; } = "D4F5C8A1-6E2B-4F3A-9C1E-2B7D8F9A0B1C";
        public string ComponentGuid => Guid;
        public ObservableCollection<IFloatingBarComponentSettingBase>? Items { get; set; } = [];

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
            Items.Add(component);
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
}
