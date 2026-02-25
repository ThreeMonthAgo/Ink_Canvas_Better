using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

/// <summary>
/// Interaction logic for NextPageControl.xaml
/// </summary>
public partial class NextPageControl : UserControl
{
    public NextPageControlVM Settings => DataContext as NextPageControlVM;

    public NextPageControl() => InitializeComponent();

    private void FloatingBarButton_Next_Click(object sender, RoutedEventArgs e) =>
        IApp.GetService<PPTService>().Next();
}
