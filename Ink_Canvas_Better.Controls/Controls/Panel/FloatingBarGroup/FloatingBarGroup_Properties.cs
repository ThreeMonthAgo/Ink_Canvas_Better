using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Ink_Canvas_Better.Controls.Panel
{
    public partial class FloatingBarGroup
    {
        public ObservableCollection<Control>? ControlsCollection { get; }

        #region Orientation


        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // It can's be used independently...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(Orientation), typeof(FloatingBarGroup), new PropertyMetadata(Orientation.Horizontal));


        #endregion
    }
}
