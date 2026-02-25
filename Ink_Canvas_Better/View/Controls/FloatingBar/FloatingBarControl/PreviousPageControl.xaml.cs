using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

/// <summary>
/// Interaction logic for PreviousPageControl.xaml
/// </summary>
public partial class PreviousPageControl : UserControl
{
    public PreviousPageControlVM Settings => DataContext as PreviousPageControlVM;

    public PreviousPageControl() => InitializeComponent();

    private void FloatingBarButton_Previous_Click(object sender, RoutedEventArgs e) =>
        IApp.GetService<PPTService>().Previous();
}
