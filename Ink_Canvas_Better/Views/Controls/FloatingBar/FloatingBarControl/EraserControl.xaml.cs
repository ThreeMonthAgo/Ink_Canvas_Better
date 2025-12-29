using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class EraserControl : UserControl, IFloatingBarComponentSettingBase
{
    private MainWindow mainWindow;

    public object Settings => DataContext as EraserControlVM;

    public EraserControl()
    {
        foreach (DictionaryEntry resource in Application.Current.Resources)
        {
            if (!this.Resources.Contains(resource.Key))
            {
                this.Resources.Add(resource.Key, resource.Value);
            }
        }
        InitializeComponent();

        this.Loaded += EeaserControl_Loaded;
    }

    private void EeaserControl_Loaded(object sender, RoutedEventArgs e)
    {
        mainWindow = App.GetService<MainWindow>();
        (Settings as EraserControlVM).IsInitializing = false;
    }

    private void EraserControl_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (mainWindow.Settings.CurrentEditingMode != Enums.EditingMode.EraseByStroke && mainWindow.Settings.CurrentEditingMode != Enums.EditingMode.EraseByPoint)
        {
            this.TryInvoke();
        }
        else
        {
            (Settings as EraserControlVM).IsOpen = true;
        }
    }

    public bool TryInvoke()
    {
        return false;
        var st = Settings as EraserControlVM;
        if (st.IsInitializing) return false;
        try
        {
            switch (st.GridViewSelectedIndex)
            {
                case 0:
                    mainWindow.Settings.CurrentEditingMode = Enums.EditingMode.EraseByStroke;
                    break;
                case 1:
                    mainWindow.Settings.CurrentDrawingAttributes.StylusTip = StylusTip.Ellipse;
                    mainWindow.MW_InkCanvas.EraserShape = new EllipseStylusShape(st.Thickness, st.Thickness);
                    mainWindow.Settings.CurrentEditingMode = Enums.EditingMode.Ink; // necessary
                    mainWindow.Settings.CurrentEditingMode = Enums.EditingMode.EraseByPoint;
                    break;
                case 2:
                    mainWindow.Settings.CurrentDrawingAttributes.StylusTip = StylusTip.Rectangle;
                    mainWindow.MW_InkCanvas.EraserShape = new RectangleStylusShape(st.Thickness, st.Thickness);
                    mainWindow.Settings.CurrentEditingMode = Enums.EditingMode.Ink; // necessary
                    mainWindow.Settings.CurrentEditingMode = Enums.EditingMode.EraseByPoint;
                    break;
                default:
                    return false;
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void Slider_Thickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => this.TryInvoke();

    private void GridView_EraserType_SelectionChanged(object sender, SelectionChangedEventArgs e) => this.TryInvoke();
}
