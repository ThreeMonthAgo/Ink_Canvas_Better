using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class PenControl : UserControl, IFloatingBarComponentSettingBase
{
    private MainWindow mainWindow;

    public object Settings => DataContext as PenControlVM;

    public PenControl()
    {
        InitializeComponent();
        this.Loaded += PenControl_Loaded;
    }

    private void PenControl_Loaded(object sender, RoutedEventArgs e)
    {
        mainWindow = App.GetService<MainWindow>();
        var st = Settings as PenControlVM;
        st.IsInitializing = false;
        st.EllipseFill = st.ColorCollection[st.GridViewSelectedIndex];
    }

    private void PenControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (mainWindow.Settings.CurrentEditingMode != Enums.EditingMode.Ink)
        {
            this.TryInvoke();
        }
        else
        {
            (Settings as PenControlVM).IsOpen = true;
        }
    }

    public bool TryInvoke()
    {
        return false;
        var st = Settings as PenControlVM;
        if (st.IsInitializing) return false;
        try
        {
            var mainWindow = App.GetService<MainWindow>();
            var seletedIndex = st.GridViewSelectedIndex;
            // UI
            st.EllipseFill = st.ColorCollection[seletedIndex];
            // InkCanvas
            mainWindow.Settings.CurrentDrawingAttributes.Color = Color.FromArgb(
                st.Alpha,
                st.ColorCollection[seletedIndex].Color.R,
                st.ColorCollection[seletedIndex].Color.G,
                st.ColorCollection[seletedIndex].Color.B
                );
            mainWindow.Settings.CurrentDrawingAttributes.StylusTip = StylusTip.Ellipse;
            mainWindow.Settings.CurrentDrawingAttributes.Width = mainWindow.Settings.CurrentDrawingAttributes.Height = Slider_Thickness.Value;
            mainWindow.Settings.CurrentEditingMode = Enums.EditingMode.Ink;
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void GridView_Colors_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var st = Settings as PenControlVM;
        if (Toggle_Color.IsChecked == true)
        {
            var seletedIndex = st.GridViewSelectedIndex;
            Popup_ColorPicker.IsOpen = false;
            Popup_ColorPicker.PlacementTarget = GridView_Colors.ItemContainerGenerator.ContainerFromIndex(seletedIndex) as UIElement;
            SqColorPicker.SelectedColor = st.ColorCollection[seletedIndex].Color;
            Popup_ColorPicker.IsOpen = true;
        }
        else if (Popup_ColorPicker.IsOpen == true) Popup_ColorPicker.IsOpen = false;
        this.TryInvoke();
    }

    private void Slider_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.TryInvoke();

    private void Slider_Alpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.TryInvoke();

    private void SqColorPicker_ColorChanged(object sender, RoutedEventArgs e)
    {
        var seletedIndex = (Settings as PenControlVM).GridViewSelectedIndex;
        (Settings as PenControlVM).ColorCollection[seletedIndex].Color = SqColorPicker.SelectedColor;
        this.TryInvoke();
    }
}
