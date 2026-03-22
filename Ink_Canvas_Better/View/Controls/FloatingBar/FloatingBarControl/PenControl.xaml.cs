using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class PenControl : UserControl
{
    public PenControlVM Settings => DataContext as PenControlVM;

    public PenControl() => InitializeComponent();

    private void PenControl_Loaded(object sender, RoutedEventArgs e)
    {
        Settings.EllipseFill = Settings.ColorCollection[Settings.GridViewSelectedIndex];
        Settings.IsInitializing = false;
    }

    private void PenControl_Click(object sender, RoutedEventArgs e) => Settings?.Click();

    private void Slider_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => Settings?.Apply();

    private void Slider_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => Settings?.Apply();

    private void GridView_Colors_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Toggle_Color.IsChecked == true)
        {
            var seletedIndex = Settings.GridViewSelectedIndex;
            Popup_ColorPicker.IsOpen = false;
            Popup_ColorPicker.PlacementTarget = GridView_Colors.ItemContainerGenerator.ContainerFromIndex(seletedIndex) as UIElement;
            SqColorPicker.SelectedColor = Settings.ColorCollection[seletedIndex].Color;
            Popup_ColorPicker.IsOpen = true;
        }
        else if (Popup_ColorPicker.IsOpen == true) Popup_ColorPicker.IsOpen = false;
        Settings.Apply();
    }

    private void SqColorPicker_ColorChanged(object sender, RoutedEventArgs e)
    {
        var seletedIndex = Settings.GridViewSelectedIndex;
        Settings.ColorCollection[seletedIndex].Color = SqColorPicker.SelectedColor;
        Settings.Apply();
    }
}
