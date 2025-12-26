using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class SettingsControl : UserControl, IFloatingBarComponentSettingBase
{
    private SettingsWindow settingsWindow;

    public static string Guid { get; } = "8AA94A7A-4847-4ED2-930F-292A7BFBA7CB";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new SettingsControlVM();

    public SettingsControl()
    {
        InitializeComponent();

        DataContext = Settings;
        this.Loaded += SettingsControl_Loaded;
        this.MouseUp += SettingsControl_MouseUp;
    }

    public bool TryInvoke() => true;

    private void SettingsControl_Loaded(object sender, RoutedEventArgs e)
    {
        this.settingsWindow = App.GetService<SettingsWindow>();
    }

    private void SettingsControl_MouseUp(object sender, MouseButtonEventArgs e) => settingsWindow.ShowWindow();
}
