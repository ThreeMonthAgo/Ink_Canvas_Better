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
    public class FloatingBarControlBase : ContentControl
    {
        static FloatingBarControlBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(typeof(FloatingBarControlBase)));

            WidthProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(64d));
            HeightProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(64d));
            BorderThicknessProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(new Thickness(2)));
        }

        #region Properties

        #region Source

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(FloatingBarControlBase), new PropertyMetadata(null));

        #endregion

        #region Text

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(FloatingBarControlBase), new PropertyMetadata("Text"));

        #endregion

        #region TextVisibility

        public Visibility TextVisibility
        {
            get { return (Visibility)GetValue(TextVisibilityProperty); }
            set { SetValue(TextVisibilityProperty, value); }
        }

        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(FloatingBarControlBase), new PropertyMetadata(Visibility.Collapsed));

        #endregion

        #region ImageWidth

        public double ImageWidth
        {
            get { return (double)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public static readonly DependencyProperty ImageWidthProperty =
            DependencyProperty.Register("ImageWidth", typeof(double), typeof(FloatingBarControlBase), new PropertyMetadata(40d));

        #endregion

        #region ImageHeight

        public double ImageHeight
        {
            get { return (double)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageHeightProperty =
            DependencyProperty.Register("ImageHeight", typeof(double), typeof(FloatingBarControlBase), new PropertyMetadata(40d));

        #endregion

        #endregion
    }
}
