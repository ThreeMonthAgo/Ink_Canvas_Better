using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class EraserControl : UserControl
{
    public EraserControlVM Settings => DataContext as EraserControlVM;

    public EraserControl()
    {
        InitializeComponent();

        this.Loaded += EeaserControl_Loaded;
    }

    private void EeaserControl_Loaded(object sender, RoutedEventArgs e)
    {
        Settings.IsInitializing = false;
    }

    private void EraserControl_Click(object sender, RoutedEventArgs e)
    {
        var mainWindowVM = IApp.GetService<SettingsService>().Settings.MainWindowVM;
        if (mainWindowVM.CurrentEditingMode != EditingMode.EraseByStroke
            && mainWindowVM.CurrentEditingMode != EditingMode.EraseByPoint)
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
            var mainWindowVM = IApp.GetService<SettingsService>().Settings.MainWindowVM;
            switch (Settings.GridViewSelectedIndex)
            {
                case 0:
                    mainWindowVM.CurrentEditingMode = EditingMode.EraseByStroke;
                    break;
                case 1:
                    mainWindowVM.CurrentDrawingAttributes.StylusTip = StylusTip.Ellipse;
                    mainWindowVM.EraserShape = new EllipseStylusShape(Settings.Thickness, Settings.Thickness);
                    mainWindowVM.CurrentEditingMode = EditingMode.Ink; // necessary
                    mainWindowVM.CurrentEditingMode = EditingMode.EraseByPoint;
                    break;
                case 2:
                    mainWindowVM.CurrentDrawingAttributes.StylusTip = StylusTip.Rectangle;
                    mainWindowVM.EraserShape = new RectangleStylusShape(Settings.Thickness, Settings.Thickness);
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
