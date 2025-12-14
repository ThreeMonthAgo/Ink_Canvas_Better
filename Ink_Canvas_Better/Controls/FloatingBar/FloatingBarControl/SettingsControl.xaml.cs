using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        this.settingsWindow = App.GetService<SettingsWindow>();
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

    #region TextVisibility

    public Visibility TextVisibility
    {
        get { return (Visibility)GetValue(TextVisibilityProperty); }
        set { SetValue(TextVisibilityProperty, value); }
    }

    public static readonly DependencyProperty TextVisibilityProperty =
        DependencyProperty.Register(nameof(TextVisibility), typeof(Visibility), typeof(SettingsControl), new PropertyMetadata(Visibility.Visible));

    #endregion

    #endregion
}

public class SettingsControlSettings
{

}
