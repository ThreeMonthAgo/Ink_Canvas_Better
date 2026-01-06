using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.View.Windows;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class EraserControl : UserControl
{
    private MainWindow mainWindow;

    public EraserControlVM Settings => DataContext as EraserControlVM;

    public EraserControl()
    {
        InitializeComponent();

        this.Loaded += EeaserControl_Loaded;
    }

    private void EeaserControl_Loaded(object sender, RoutedEventArgs e)
    {
        mainWindow = IApp.GetService<MainWindow>();
        Settings.IsInitializing = false;
    }

    private void EraserControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (mainWindow.Settings.CurrentEditingMode != EditingMode.EraseByStroke
            && mainWindow.Settings.CurrentEditingMode != EditingMode.EraseByPoint)
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
            var mainWindowVM = mainWindow.Settings;
            switch (Settings.GridViewSelectedIndex)
            {
                case 0:
                    mainWindowVM.CurrentEditingMode = EditingMode.EraseByStroke;
                    break;
                case 1:
                    mainWindowVM.CurrentDrawingAttributes.StylusTip = StylusTip.Ellipse;
                    mainWindow.InkCanvas.EraserShape = new EllipseStylusShape(Settings.Thickness, Settings.Thickness);
                    mainWindowVM.CurrentEditingMode = EditingMode.Ink; // necessary
                    mainWindowVM.CurrentEditingMode = EditingMode.EraseByPoint;
                    break;
                case 2:
                    mainWindowVM.CurrentDrawingAttributes.StylusTip = StylusTip.Rectangle;
                    mainWindow.InkCanvas.EraserShape = new RectangleStylusShape(Settings.Thickness, Settings.Thickness);
                    mainWindowVM.CurrentEditingMode = EditingMode.Ink; // necessary
                    mainWindowVM.CurrentEditingMode = EditingMode.EraseByPoint;
                    break;
            }
        }
        catch (Exception) { }
    }

    private void Slider_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.Apply();

    private void GridView_EraserType_SelectionChanged(object sender, SelectionChangedEventArgs e) => this.Apply();
}
