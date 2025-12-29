using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class SettingsControl : UserControl, IFloatingBarComponentSettingBase
{
    private SettingsWindow settingsWindow;

    public object Settings => DataContext as SettingsControlVM;

    public SettingsControl()
    {
        foreach (DictionaryEntry resource in Application.Current.Resources)
        {
            if (!this.Resources.Contains(resource.Key))
            {
                this.Resources.Add(resource.Key, resource.Value);
            }
        }
        InitializeComponent();

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
