using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class SettingsControl : UserControl
{
    private readonly SettingsService settingsService = IApp.GetService<SettingsService>();

    public SettingsControlVM Settings => DataContext as SettingsControlVM;

    public SettingsControl() => InitializeComponent();

    private void SettingsControl_Loaded(object sender, RoutedEventArgs e) => Settings.IsInitializing = false;

    private void SettingsControl_Click(object sender, RoutedEventArgs e) => settingsService.ShowSettingsWindow();
}
