using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ink_Canvas_Better.Controls.FloatingBar;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Windows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        FloatingBar floatingBar;
        ControlsService controlsService;

        public MainWindow(FloatingBar floatingBar, ControlsService controlsService)
        {
            InitializeComponent();

            this.floatingBar = floatingBar;
            this.controlsService = controlsService;
            // Temp
            MW_Grid.Children.Add(floatingBar);
            ((FloatingBarSettings)(this.floatingBar.Settings)).Items.Add(controlsService.CreateControl(MultifunctionControl.Guid));
            Debug.WriteLine(((FloatingBarSettings)(this.floatingBar.Settings)).Items[0]);
        }

    }
}
