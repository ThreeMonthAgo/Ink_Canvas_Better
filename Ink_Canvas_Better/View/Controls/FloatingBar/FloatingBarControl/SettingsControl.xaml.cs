using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.View.Windows;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class SettingsControl : UserControl
{
    private SettingsWindow settingsWindow;

    public SettingsControlVM Settings => DataContext as SettingsControlVM;

    public SettingsControl()
    {
        InitializeComponent();

        this.Loaded += SettingsControl_Loaded;
    }

    private void SettingsControl_Loaded(object sender, RoutedEventArgs e)
    {
        Settings.IsInitializing = false;
        this.settingsWindow = IApp.GetService<SettingsWindow>();
    }

    private void SettingsControl_MouseUp(object sender, MouseButtonEventArgs e) => settingsWindow.ShowWindow();
}
