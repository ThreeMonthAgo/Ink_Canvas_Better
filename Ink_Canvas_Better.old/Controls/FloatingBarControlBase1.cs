//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Controls.Primitives;
//using System.Windows.Media;

//namespace Ink_Canvas_Better.Controls
//{
//    public class FloatingBarControlBase1 : ContentControl
//    {
//        static FloatingBarControlBase1()
//        {
//            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(typeof(FloatingBarControlBase)));

//            WidthProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(64d));
//            HeightProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(64d));
//            BorderThicknessProperty.OverrideMetadata(typeof(FloatingBarControlBase), new FrameworkPropertyMetadata(new Thickness(2)));
//        }

//        #region Properties

//        #region Source

//        public ImageSource Source
//        {
//            get { return (ImageSource)GetValue(SourceProperty); }
//            set { SetValue(SourceProperty, value); }
//        }

//        public static readonly DependencyProperty SourceProperty =
//            DependencyProperty.Register("Source", typeof(ImageSource), typeof(FloatingBarControlBase), new PropertyMetadata(null));

//        #endregion

//        #region Text

//        public string Text
//        {
//            get { return (string)GetValue(TextProperty); }
//            set { SetValue(TextProperty, value); }
//        }

//        public static readonly DependencyProperty TextProperty =
//            DependencyProperty.Register("Text", typeof(string), typeof(FloatingBarControlBase), new PropertyMetadata("Text"));

//        #endregion

//        #region TextVisibility

//        public Visibility TextVisibility
//        {
//            get { return (Visibility)GetValue(TextVisibilityProperty); }
//            set { SetValue(TextVisibilityProperty, value); }
//        }

//        public static readonly DependencyProperty TextVisibilityProperty =
//            DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(FloatingBarControlBase), new PropertyMetadata(Visibility.Collapsed));

//        #endregion

//        #region ImageWidth

//        public double ImageWidth
//        {
//            get { return (double)GetValue(ImageWidthProperty); }
//            set { SetValue(ImageWidthProperty, value); }
//        }

//        public static readonly DependencyProperty ImageWidthProperty =
//            DependencyProperty.Register("ImageWidth", typeof(double), typeof(FloatingBarControlBase), new PropertyMetadata(40d));

//        #endregion

//        #region ImageHeight

//        public double ImageHeight
//        {
//            get { return (double)GetValue(ImageHeightProperty); }
//            set { SetValue(ImageHeightProperty, value); }
//        }

//        public static readonly DependencyProperty ImageHeightProperty =
//            DependencyProperty.Register("ImageHeight", typeof(double), typeof(FloatingBarControlBase), new PropertyMetadata(40d));

//        #endregion

//        // Popup Properties

//        #region Title

//        public string Title
//        {
//            get { return (string)GetValue(TitleProperty); }
//            set { SetValue(TitleProperty, value); }
//        }

//        public static readonly DependencyProperty TitleProperty =
//            DependencyProperty.Register(nameof(Title), typeof(string), typeof(FloatingBarControlBase), new PropertyMetadata("Subpanel"));

//        #endregion

//        #region IsOpen

//        public bool IsOpen
//        {
//            get { return (bool)GetValue(IsOpenProperty); }
//            set { SetValue(IsOpenProperty, value); }
//        }

//        public static readonly DependencyProperty IsOpenProperty =
//            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(FloatingBarControlBase), new PropertyMetadata(false));

//        #endregion

//        #region StaysOpen

//        public bool StaysOpen
//        {
//            get { return (bool)GetValue(StaysOpenProperty); }
//            set { SetValue(StaysOpenProperty, value); }
//        }

//        public static readonly DependencyProperty StaysOpenProperty =
//            DependencyProperty.Register(nameof(StaysOpen), typeof(bool), typeof(FloatingBarControlBase), new PropertyMetadata(false));

//        #endregion

//        #region PlacementTarget

//        public UIElement PlacementTarget
//        {
//            get { return (UIElement)GetValue(PlacementTargetProperty); }
//            set { SetValue(PlacementTargetProperty, value); }
//        }

//        public static readonly DependencyProperty PlacementTargetProperty =
//            DependencyProperty.Register(nameof(PlacementTarget), typeof(UIElement), typeof(FloatingBarControlBase), new PropertyMetadata(null));

//        #endregion

//        #region TitleBarHeight

//        public double TitleBarHeight
//        {
//            get { return (double)GetValue(TitleBarHeightProperty); }
//            set { SetValue(TitleBarHeightProperty, value); }
//        }

//        public static readonly DependencyProperty TitleBarHeightProperty =
//            DependencyProperty.Register(nameof(TitleBarHeight), typeof(double), typeof(FloatingBarControlBase), new PropertyMetadata(36d));

//        #endregion

//        #region CornerRadius

//        public CornerRadius CornerRadius
//        {
//            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
//            set { SetValue(CornerRadiusProperty, value); }
//        }

//        public static readonly DependencyProperty CornerRadiusProperty =
//            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(FloatingBarControlBase), new PropertyMetadata(new CornerRadius(4d)));

//        #endregion

//        #region Placement

//        public PlacementMode Placement
//        {
//            get { return (PlacementMode)GetValue(PlacementProperty); }
//            set { SetValue(PlacementProperty, value); }
//        }

//        public static readonly DependencyProperty PlacementProperty =
//            DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(FloatingBarControlBase), new PropertyMetadata(PlacementMode.Top));

//        #endregion

//        #region PopupAnimation

//        public PopupAnimation PopupAnimation
//        {
//            get { return (PopupAnimation)GetValue(PopupAnimationProperty); }
//            set { SetValue(PopupAnimationProperty, value); }
//        }

//        public static readonly DependencyProperty PopupAnimationProperty =
//            DependencyProperty.Register(nameof(PopupAnimation), typeof(PopupAnimation), typeof(FloatingBarControlBase), new PropertyMetadata(PopupAnimation.Slide));

//        #endregion

//        #region PlacementRectangle

//        public Rect PlacementRectangle
//        {
//            get { return (Rect)GetValue(PlacementRectangleProperty); }
//            set { SetValue(PlacementRectangleProperty, value); }
//        }

//        public static readonly DependencyProperty PlacementRectangleProperty =
//            DependencyProperty.Register(nameof(PlacementRectangle), typeof(Rect), typeof(FloatingBarControlBase), new PropertyMetadata(new Rect(0, 0, 0, 0)));

//        #endregion

//        #endregion
//    }
//}
