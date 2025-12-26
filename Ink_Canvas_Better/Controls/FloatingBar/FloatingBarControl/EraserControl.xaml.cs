using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.ViewModels.Windows;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl
{
    /// <summary>
    /// EeaserControl.xaml 的交互逻辑
    /// </summary>
    public partial class EraserControl : UserControl, IFloatingBarComponentSettingBase
    {
        private MainWindow mainWindow;

        public static string Guid { get; } = "F4A558A1-ABF1-4493-8D14-8D0D18363B72";
        public string ComponentGuid => Guid;
        public object Settings { get; set; } = new EraserControlVM();

        public EraserControl()
        {
            InitializeComponent();

            DataContext = Settings;
            this.Loaded += EeaserControl_Loaded;
        }

        private void EeaserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow = App.GetService<MainWindow>();
            (Settings as EraserControlVM).IsInitializing = false;
        }

        private void EraserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (mainWindow.CurrentEditingMode != Enums.EditingMode.EraseByStroke && mainWindow.CurrentEditingMode != Enums.EditingMode.EraseByPoint)
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
            var st = Settings as EraserControlVM;
            if (st.IsInitializing) return false;
            try
            {
                switch (st.GridViewSelectedIndex)
                {
                    case 0:
                        mainWindow.CurrentEditingMode = Enums.EditingMode.EraseByStroke;
                        break;
                    case 1:
                        mainWindow.Settings.CurrentDrawingAttributes.StylusTip = StylusTip.Ellipse;
                        mainWindow.MW_InkCanvas.EraserShape = new EllipseStylusShape(st.Thickness, st.Thickness);
                        mainWindow.CurrentEditingMode = Enums.EditingMode.Ink; // necessary
                        mainWindow.CurrentEditingMode = Enums.EditingMode.EraseByPoint;
                        break;
                    case 2:
                        mainWindow.Settings.CurrentDrawingAttributes.StylusTip = StylusTip.Rectangle;
                        mainWindow.MW_InkCanvas.EraserShape = new RectangleStylusShape(st.Thickness, st.Thickness);
                        mainWindow.CurrentEditingMode = Enums.EditingMode.Ink; // necessary
                        mainWindow.CurrentEditingMode = Enums.EditingMode.EraseByPoint;
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
}
