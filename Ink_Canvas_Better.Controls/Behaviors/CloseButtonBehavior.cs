using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Xaml.Behaviors;
using LogicalTreeHelper = Ink_Canvas_Better.Controls.Helpers.LogicalTreeHelper;

namespace Ink_Canvas_Better.Controls.Behaviors
{
    /// <summary>
    /// Used for <see cref="Popup"/>.
    /// </summary>
    public class CloseButtonBehavior : Behavior<Button>
    {
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(CloseButtonBehavior), new PropertyMetadata(true));

        protected override void OnAttached()
        {
            base.OnAttached();
            if (IsEnabled)
            {
                AssociatedObject.Click += AssociatedObject_Click;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= AssociatedObject_Click;
        }

        private void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            var popup = LogicalTreeHelper.FindAscendant<Popup>(AssociatedObject);
            popup.IsOpen = false;
        }
    }
}
