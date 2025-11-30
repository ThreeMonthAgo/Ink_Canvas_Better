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
using Ink_Canvas_Better.Interface;

namespace Ink_Canvas_Better.Controls.FloatingBar
{
    /// <summary>
    /// FloatingBarGroup.xaml 的交互逻辑
    /// </summary>
    public partial class FloatingBarGroup : FloatingBarComponentBase, IFloatingBarComponentSettingBase
    {
        public object Settings { get; set; } = new FloatingBarGroupSettings();
        public static string Guid { get; } = "B1E2F3A4-5678-90AB-CDEF-1234567890AB";

        public FloatingBarGroup()
        {
            InitializeComponent();
        }

        public void Add(IFloatingBarComponentSettingBase component)
        {
            ((FloatingBarGroupSettings)Settings).Items.Add(component);
        }

        #region dp

        #region Orientation

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(FloatingBarGroup), new PropertyMetadata(Orientation.Horizontal));

        #endregion

        #region Spacing

        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public static readonly DependencyProperty SpacingProperty =
            DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(FloatingBarGroup), new PropertyMetadata(0d));

        #endregion

        #endregion

    }

    public class FloatingBarGroupSettings
    {
        public List<IFloatingBarComponentSettingBase> Items { get; set; } = [];
    }
}
