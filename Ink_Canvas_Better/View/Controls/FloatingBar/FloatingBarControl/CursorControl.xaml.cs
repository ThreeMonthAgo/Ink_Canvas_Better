using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class CursorControl : UserControl
{
    public CursorControlVM Settings => DataContext as CursorControlVM;

    public CursorControl() => InitializeComponent();

    private void CursorControl_Click(object sender, RoutedEventArgs e) => Apply();

    private void CursorControl_Loaded(object sender, RoutedEventArgs e) => Settings.IsInitializing = false;

    public void Apply() => IApp.GetService<SettingsService>().Settings.MainWindowVM.CurrentEditingMode = EditingMode.None;
}
