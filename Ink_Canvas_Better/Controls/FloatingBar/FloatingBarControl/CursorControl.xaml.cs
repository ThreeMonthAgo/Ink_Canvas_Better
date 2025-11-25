using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ink_Canvas_Better.Interface;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
/// <summary>
/// The logic for CursorControl.xaml
/// </summary>
public class CursorControl : Control, IFloatingBarComponentSettingBase
{
    public object Settings { get; set; } = new CursorControlSettings();
    private static Guid _guid = new("{9A703354-E315-4FFE-BB3A-503E0B901DCC}");
    public static Guid Guid => _guid;

    static CursorControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CursorControl), new FrameworkPropertyMetadata(typeof(CursorControl)));
    }

    #region Properties

    #region Source

    public ImageSource Source
    {
        get { return (ImageSource)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(ImageSource), typeof(CursorControl), new PropertyMetadata(null));

    #endregion

    #region Text

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(CursorControl), new PropertyMetadata("Text"));

    #endregion

    #region TextVisibility

    public Visibility TextVisibility
    {
        get { return (Visibility)GetValue(TextVisibilityProperty); }
        set { SetValue(TextVisibilityProperty, value); }
    }

    public static readonly DependencyProperty TextVisibilityProperty =
        DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(CursorControl), new PropertyMetadata(Visibility.Collapsed));

    #endregion

    #region ImageWidth

    public double ImageWidth
    {
        get { return (double)GetValue(ImageWidthProperty); }
        set { SetValue(ImageWidthProperty, value); }
    }

    public static readonly DependencyProperty ImageWidthProperty =
        DependencyProperty.Register("ImageWidth", typeof(double), typeof(CursorControl), new PropertyMetadata(40d));

    #endregion

    #region ImageHeight

    public double ImageHeight
    {
        get { return (double)GetValue(ImageHeightProperty); }
        set { SetValue(ImageHeightProperty, value); }
    }

    public static readonly DependencyProperty ImageHeightProperty =
        DependencyProperty.Register("ImageHeight", typeof(double), typeof(CursorControl), new PropertyMetadata(40d));

    #endregion

    // Popup Properties

    #region Title

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(CursorControl), new PropertyMetadata("Subpanel"));

    #endregion

    #region IsOpen

    public bool IsOpen
    {
        get { return (bool)GetValue(IsOpenProperty); }
        set { SetValue(IsOpenProperty, value); }
    }

    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(CursorControl), new PropertyMetadata(false));

    #endregion

    #region StaysOpen

    public bool StaysOpen
    {
        get { return (bool)GetValue(StaysOpenProperty); }
        set { SetValue(StaysOpenProperty, value); }
    }

    public static readonly DependencyProperty StaysOpenProperty =
        DependencyProperty.Register(nameof(StaysOpen), typeof(bool), typeof(CursorControl), new PropertyMetadata(false));

    #endregion

    #region PlacementTarget

    public UIElement PlacementTarget
    {
        get { return (UIElement)GetValue(PlacementTargetProperty); }
        set { SetValue(PlacementTargetProperty, value); }
    }

    public static readonly DependencyProperty PlacementTargetProperty =
        DependencyProperty.Register(nameof(PlacementTarget), typeof(UIElement), typeof(CursorControl), new PropertyMetadata(null));

    #endregion

    #region TitleBarHeight

    public double TitleBarHeight
    {
        get { return (double)GetValue(TitleBarHeightProperty); }
        set { SetValue(TitleBarHeightProperty, value); }
    }

    public static readonly DependencyProperty TitleBarHeightProperty =
        DependencyProperty.Register(nameof(TitleBarHeight), typeof(double), typeof(CursorControl), new PropertyMetadata(36d));

    #endregion

    #region CornerRadius

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(CursorControl), new PropertyMetadata(new CornerRadius(4d)));

    #endregion

    #region Placement

    public PlacementMode Placement
    {
        get { return (PlacementMode)GetValue(PlacementProperty); }
        set { SetValue(PlacementProperty, value); }
    }

    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(CursorControl), new PropertyMetadata(PlacementMode.Top));

    #endregion

    #region PopupAnimation

    public PopupAnimation PopupAnimation
    {
        get { return (PopupAnimation)GetValue(PopupAnimationProperty); }
        set { SetValue(PopupAnimationProperty, value); }
    }

    public static readonly DependencyProperty PopupAnimationProperty =
        DependencyProperty.Register(nameof(PopupAnimation), typeof(PopupAnimation), typeof(CursorControl), new PropertyMetadata(PopupAnimation.Slide));

    #endregion

    #region PlacementRectangle

    public Rect PlacementRectangle
    {
        get { return (Rect)GetValue(PlacementRectangleProperty); }
        set { SetValue(PlacementRectangleProperty, value); }
    }

    public static readonly DependencyProperty PlacementRectangleProperty =
        DependencyProperty.Register(nameof(PlacementRectangle), typeof(Rect), typeof(CursorControl), new PropertyMetadata(new Rect(0, 0, 0, 0)));

    #endregion

    #endregion
}