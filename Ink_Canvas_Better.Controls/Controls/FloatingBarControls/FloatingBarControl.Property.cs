using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using iNKORE.UI.WPF.Modern.Common.IconKeys;
using iNKORE.UI.WPF.Modern.Controls;

namespace Ink_Canvas_Better.Controls.FloatingBarControls
{
    public partial class FloatingBarControl
    {
        #region Propties

        #region Text

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(FloatingBarControl), new PropertyMetadata("Text"));

        #endregion

        #region TextVisibility

        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register(nameof(TextVisibility), typeof(Visibility), typeof(FloatingBarControl), new PropertyMetadata(Visibility.Visible));

        #endregion

        #region ImageSource

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(FloatingBarControl), new PropertyMetadata(null));

        #endregion

        #region Icon

        public FontIconData Icon
        {
            get { return (FontIconData)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register(nameof(Icon), typeof(FontIconData), typeof(FloatingBarControl), new PropertyMetadata(null));

        #endregion

        #region ExtraContent

        public object ExtraContent
        {
            get { return (object)GetValue(ExtraContentProperty); }
            set { SetValue(ExtraContentProperty, value); }
        }

        public static readonly DependencyProperty ExtraContentProperty =
            DependencyProperty.Register(nameof(ExtraContent), typeof(object), typeof(FloatingBarControl), new PropertyMetadata(null));

        #endregion

        #region PopupContent

        public object PopupContent
        {
            get { return (object)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }

        public static readonly DependencyProperty PopupContentProperty =
            DependencyProperty.Register(nameof(PopupContent), typeof(object), typeof(FloatingBarControl), new PropertyMetadata(null));

        #endregion

        #region IsOpen

        /// <remarks>
        /// Please bind this property in TwoWay mode, the popup will not perform correctly otherwise.
        /// e.g.
        /// <code>
        /// IsOpen="{Binding IsOpen, Mode=TwoWay}"
        /// </code>
        /// </remarks>
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(FloatingBarControl), new PropertyMetadata(false));

        #endregion

        #region StaysOpen

        public bool StaysOpen
        {
            get { return (bool)GetValue(StaysOpenProperty); }
            set { SetValue(StaysOpenProperty, value); }
        }

        public static readonly DependencyProperty StaysOpenProperty =
            DependencyProperty.Register(nameof(StaysOpen), typeof(bool), typeof(FloatingBarControl), new PropertyMetadata(false, OnStaysOpenChanged));

        private static void OnStaysOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var floatingBarControl = d as FloatingBarControl;
            var fontIcon = floatingBarControl.GetTemplateChild("PART_ToggleButton_FontIcon") as FontIcon;
            fontIcon.Icon = (bool)(e.NewValue) ? SegoeFluentIcons.Pin : SegoeFluentIcons.Unpin;
        }

        #endregion

        #endregion

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        public static readonly RoutedEvent ClickEvent =
            EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FloatingBarControl));
    }
}
    