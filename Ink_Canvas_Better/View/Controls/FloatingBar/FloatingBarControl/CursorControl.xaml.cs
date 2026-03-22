using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class CursorControl : UserControl
{
    public CursorControlVM Settings => DataContext as CursorControlVM;

    public CursorControl() => InitializeComponent();

    private void CursorControl_Loaded(object sender, RoutedEventArgs e) => Settings.IsInitializing = false;

    private void CursorControl_Click(object sender, RoutedEventArgs e) => Settings?.Apply();
}
