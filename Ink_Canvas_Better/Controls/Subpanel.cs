using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Ink_Canvas_Better.Controls.Helpers;

namespace Ink_Canvas_Better.Controls
{
    public class Subpanel : ContentControl
    {
        const double IconSize = 24d;

        static Subpanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Subpanel), new FrameworkPropertyMetadata(typeof(Subpanel)));
        }

        #region Properties

        #region Title

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(Subpanel), new PropertyMetadata("Subpanel"));

        #endregion

        #region IsOpen

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(Subpanel), new PropertyMetadata(false));

        #endregion

        #region StaysOpen

        public bool StaysOpen
        {
            get { return (bool)GetValue(StaysOpenProperty); }
            set { SetValue(StaysOpenProperty, value); }
        }

        public static readonly DependencyProperty StaysOpenProperty =
            DependencyProperty.Register(nameof(StaysOpen), typeof(bool), typeof(Subpanel), new PropertyMetadata(false));

        #endregion

        #region PlacementTarget

        public UIElement PlacementTarget
        {
            get { return (UIElement)GetValue(PlacementTargetProperty); }
            set { SetValue(PlacementTargetProperty, value); }
        }

        public static readonly DependencyProperty PlacementTargetProperty =
            DependencyProperty.Register(nameof(PlacementTarget), typeof(UIElement), typeof(Subpanel), new PropertyMetadata(null));

        #endregion

        #region TitleBarHeight

        public double TitleBarHeight
        {
            get { return (double)GetValue(TitleBarHeightProperty); }
            set { SetValue(TitleBarHeightProperty, value); }
        }

        public static readonly DependencyProperty TitleBarHeightProperty =
            DependencyProperty.Register(nameof(TitleBarHeight), typeof(double), typeof(Subpanel), new PropertyMetadata(36d));

        #endregion

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Subpanel), new PropertyMetadata(new CornerRadius(4d)));

        #endregion

        #region AllowsTransparency

        public bool AllowsTransparency
        {
            get { return (bool)GetValue(AllowsTransparencyProperty); }
            set { SetValue(AllowsTransparencyProperty, value); }
        }

        public static readonly DependencyProperty AllowsTransparencyProperty =
            DependencyProperty.Register(nameof(AllowsTransparency), typeof(bool), typeof(Subpanel), new PropertyMetadata(true));

        #endregion

        #region Placement

        public PlacementMode Placement
        {
            get { return (PlacementMode)GetValue(PlacementProperty); }
            set { SetValue(PlacementProperty, value); }
        }

        public static readonly DependencyProperty PlacementProperty =
            DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(Subpanel), new PropertyMetadata(PlacementMode.Top));

        #endregion

        #region PopupAnimation

        public PopupAnimation PopupAnimation
        {
            get { return (PopupAnimation)GetValue(PopupAnimationProperty); }
            set { SetValue(PopupAnimationProperty, value); }
        }

        public static readonly DependencyProperty PopupAnimationProperty =
            DependencyProperty.Register(nameof(PopupAnimation), typeof(PopupAnimation), typeof(Subpanel), new PropertyMetadata(PopupAnimation.Slide));

        #endregion

        #region PlacementRectangle

        public Rect PlacementRectangle
        {
            get { return (Rect)GetValue(PlacementRectangleProperty); }
            set { SetValue(PlacementRectangleProperty, value); }
        }

        public static readonly DependencyProperty PlacementRectangleProperty =
            DependencyProperty.Register(nameof(PlacementRectangle), typeof(Rect), typeof(Subpanel), new PropertyMetadata(new Rect(0, 0, 0, 0)));

        #endregion

        #endregion

        public virtual void CloseButton_Click(Object sender, RoutedEventArgs args)
        {
            this.IsOpen = false;
            var closeButton = GetTemplateChild("PART_CloseButton") as Button;
            closeButton.Content = new Image() { Source = ThemeHelper.FUI_Dismiss, Height = IconSize, Width = IconSize };
        }

        public virtual void PinToggleButton_Checked(Object sender, RoutedEventArgs args)
        {
            this.StaysOpen = true;
            var pinToggleButton = GetTemplateChild("PART_PinToggleButton") as ToggleButton;
            pinToggleButton.Content = new Image() { Source = ThemeHelper.FUI_PinOff, Height = IconSize, Width = IconSize };
        }

        public virtual void PinToggleButton_Unchecked(Object sender, RoutedEventArgs args)
        {
            this.StaysOpen = false;
            var pinToggleButton = GetTemplateChild("PART_PinToggleButton") as ToggleButton;
            pinToggleButton.Content = new Image() { Source = ThemeHelper.FUI_Pin, Height = IconSize, Width = IconSize };
        }
    }
}
