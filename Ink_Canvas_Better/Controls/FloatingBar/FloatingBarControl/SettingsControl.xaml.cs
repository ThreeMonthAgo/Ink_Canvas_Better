using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
/// <summary>
/// SettingsControl.xaml 的交互逻辑
/// </summary>
public partial class SettingsControl : UserControl, IFloatingBarComponentSettingBase
{
    private SettingsWindow settingsWindow;

    public static string Guid { get; } = "8AA94A7A-4847-4ED2-930F-292A7BFBA7CB";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new SettingsControlSettings();
    public SettingsControlSettings SettingsControlSettings => Settings as SettingsControlSettings;

    public SettingsControl()
    {
        InitializeComponent();

        this.Loaded += SettingsControl_Loaded;
        this.MouseUp += SettingsControl_MouseUp;
    }

    private void SettingsControl_Loaded(object sender, RoutedEventArgs e)
    {
        this.settingsWindow = Program.GetService<SettingsWindow>();
    }

    private void SettingsControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (settingsWindow.IsActive)
        {
            settingsWindow.Focus();
        }
        else
        {
            settingsWindow.Show();
            settingsWindow.Activate();
        }
    }

    #region Properties

    #region Source

    public ImageSource Source
    {
        get { return (ImageSource)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(ImageSource), typeof(SettingsControl), new PropertyMetadata(null));

    #endregion

    #region Text

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(SettingsControl), new PropertyMetadata("Text"));

    #endregion

    #region TextVisibility

    public Visibility TextVisibility
    {
        get { return (Visibility)GetValue(TextVisibilityProperty); }
        set { SetValue(TextVisibilityProperty, value); }
    }

    public static readonly DependencyProperty TextVisibilityProperty =
        DependencyProperty.Register("TextVisibility", typeof(Visibility), typeof(SettingsControl), new PropertyMetadata(Visibility.Collapsed));

    #endregion

    #region ImageWidth

    public double ImageWidth
    {
        get { return (double)GetValue(ImageWidthProperty); }
        set { SetValue(ImageWidthProperty, value); }
    }

    public static readonly DependencyProperty ImageWidthProperty =
        DependencyProperty.Register("ImageWidth", typeof(double), typeof(SettingsControl), new PropertyMetadata(40d));

    #endregion

    #region ImageHeight

    public double ImageHeight
    {
        get { return (double)GetValue(ImageHeightProperty); }
        set { SetValue(ImageHeightProperty, value); }
    }

    public static readonly DependencyProperty ImageHeightProperty =
        DependencyProperty.Register("ImageHeight", typeof(double), typeof(SettingsControl), new PropertyMetadata(40d));

    #endregion

    // Popup Properties

    #region Title

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(nameof(Title), typeof(string), typeof(SettingsControl), new PropertyMetadata("Subpanel"));

    #endregion

    #region IsOpen

    public bool IsOpen
    {
        get { return (bool)GetValue(IsOpenProperty); }
        set { SetValue(IsOpenProperty, value); }
    }

    public static readonly DependencyProperty IsOpenProperty =
        DependencyProperty.Register(nameof(IsOpen), typeof(bool), typeof(SettingsControl), new PropertyMetadata(false));

    #endregion

    #region StaysOpen

    public bool StaysOpen
    {
        get { return (bool)GetValue(StaysOpenProperty); }
        set { SetValue(StaysOpenProperty, value); }
    }

    public static readonly DependencyProperty StaysOpenProperty =
        DependencyProperty.Register(nameof(StaysOpen), typeof(bool), typeof(SettingsControl), new PropertyMetadata(false));

    #endregion

    #region PlacementTarget

    public UIElement PlacementTarget
    {
        get { return (UIElement)GetValue(PlacementTargetProperty); }
        set { SetValue(PlacementTargetProperty, value); }
    }

    public static readonly DependencyProperty PlacementTargetProperty =
        DependencyProperty.Register(nameof(PlacementTarget), typeof(UIElement), typeof(SettingsControl), new PropertyMetadata(null));

    #endregion

    #region TitleBarHeight

    public double TitleBarHeight
    {
        get { return (double)GetValue(TitleBarHeightProperty); }
        set { SetValue(TitleBarHeightProperty, value); }
    }

    public static readonly DependencyProperty TitleBarHeightProperty =
        DependencyProperty.Register(nameof(TitleBarHeight), typeof(double), typeof(SettingsControl), new PropertyMetadata(36d));

    #endregion

    #region CornerRadius

    public CornerRadius CornerRadius
    {
        get { return (CornerRadius)GetValue(CornerRadiusProperty); }
        set { SetValue(CornerRadiusProperty, value); }
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(SettingsControl), new PropertyMetadata(new CornerRadius(4d)));

    #endregion

    #region Placement

    public PlacementMode Placement
    {
        get { return (PlacementMode)GetValue(PlacementProperty); }
        set { SetValue(PlacementProperty, value); }
    }

    public static readonly DependencyProperty PlacementProperty =
        DependencyProperty.Register(nameof(Placement), typeof(PlacementMode), typeof(SettingsControl), new PropertyMetadata(PlacementMode.Top));

    #endregion

    #region PopupAnimation

    public PopupAnimation PopupAnimation
    {
        get { return (PopupAnimation)GetValue(PopupAnimationProperty); }
        set { SetValue(PopupAnimationProperty, value); }
    }

    public static readonly DependencyProperty PopupAnimationProperty =
        DependencyProperty.Register(nameof(PopupAnimation), typeof(PopupAnimation), typeof(SettingsControl), new PropertyMetadata(PopupAnimation.Slide));

    #endregion

    #region PlacementRectangle

    public Rect PlacementRectangle
    {
        get { return (Rect)GetValue(PlacementRectangleProperty); }
        set { SetValue(PlacementRectangleProperty, value); }
    }

    public static readonly DependencyProperty PlacementRectangleProperty =
        DependencyProperty.Register(nameof(PlacementRectangle), typeof(Rect), typeof(SettingsControl), new PropertyMetadata(new Rect(0, 0, 0, 0)));

    #endregion

    #endregion
}

public class SettingsControlSettings
{

}

