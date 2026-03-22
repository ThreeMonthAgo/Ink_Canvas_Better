using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class EraserControl : UserControl
{
    public EraserControlVM Settings => DataContext as EraserControlVM;

    public EraserControl() => InitializeComponent();

    private void EeaserControl_Loaded(object sender, RoutedEventArgs e) => Settings.IsInitializing = false;

    private void EraserControl_Click(object sender, RoutedEventArgs e) => Settings?.Click();

    private void Slider_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => Settings?.Apply();

    private void GridView_EraserType_SelectionChanged(object sender, SelectionChangedEventArgs e) => Settings?.Apply();

    private void EraserControl_Clear(object sender, RoutedEventArgs e) => Settings?.Clear();
}
