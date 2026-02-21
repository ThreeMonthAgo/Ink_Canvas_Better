using System.Windows;
using System.Windows.Media;
using iNKORE.UI.WPF.Modern.Common.IconKeys;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    public partial class FloatingBarButton
    {
        #region Propties

        #region Text

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(FloatingBarButton), new PropertyMetadata("Text"));

        #endregion

        #region TextVisibility

        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register(nameof(TextVisibility), typeof(Visibility), typeof(FloatingBarButton), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region ImageSource

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(FloatingBarButton), new PropertyMetadata(null));

        #endregion

        #region Icon

        public FontIconData Icon
        {
            get { return (FontIconData)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(FontIconData), typeof(FloatingBarButton), new PropertyMetadata(null));

        #endregion

        #region ExtraContent

        public object ExtraContent
        {
            get { return (object)GetValue(ExtraContentProperty); }
            set { SetValue(ExtraContentProperty, value); }
        }

        public static readonly DependencyProperty ExtraContentProperty =
            DependencyProperty.Register(nameof(ExtraContent), typeof(object), typeof(FloatingBarButton), new PropertyMetadata(null));

        #endregion

        #endregion

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FloatingBarButton));
    }
}
    