using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class PenControl : UserControl
{
    private MainWindow mainWindow;

    public PenControlVM Settings => DataContext as PenControlVM;

    public PenControl()
    {
        InitializeComponent();
        this.Loaded += PenControl_Loaded;
    }

    private void PenControl_Loaded(object sender, RoutedEventArgs e)
    {
        mainWindow = IApp.GetService<MainWindow>();
        Settings.EllipseFill = Settings.ColorCollection[Settings.GridViewSelectedIndex];
        Settings.IsInitializing = false;
    }

    private void PenControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (mainWindow.Settings.CurrentEditingMode != EditingMode.Ink)
        {
            this.Apply();
        }
        else
        {
            Settings.IsOpen = true;
        }
    }

    public void Apply()
    {
        if (Settings == null || Settings.IsInitializing) return;
        try
        {
            var seletedIndex = Settings.GridViewSelectedIndex;
            var mainWindowVM = mainWindow.Settings;
            // UI
            Settings.EllipseFill = Settings.ColorCollection[seletedIndex];
            // InkCanvas
            mainWindowVM.CurrentDrawingAttributes.Color = Color.FromArgb(
                Settings.Alpha,
                Settings.ColorCollection[seletedIndex].Color.R,
                Settings.ColorCollection[seletedIndex].Color.G,
                Settings.ColorCollection[seletedIndex].Color.B
                );
            mainWindowVM.CurrentDrawingAttributes.StylusTip = StylusTip.Ellipse;
            mainWindowVM.CurrentDrawingAttributes.Width = mainWindowVM.CurrentDrawingAttributes.Height = Slider_Thickness.Value;
            mainWindowVM.CurrentEditingMode = EditingMode.Ink;
        }
        catch (Exception) { }
    }

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
        this.Apply();
    }

    private void Slider_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.Apply();

    private void Slider_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.Apply();

    private void SqColorPicker_ColorChanged(object sender, RoutedEventArgs e)
    {
        var seletedIndex = Settings.GridViewSelectedIndex;
        Settings.ColorCollection[seletedIndex].Color = SqColorPicker.SelectedColor;
        this.Apply();
    }
}
