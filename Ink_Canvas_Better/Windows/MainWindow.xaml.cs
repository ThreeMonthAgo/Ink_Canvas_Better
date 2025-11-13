using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Controls.FloatingBarControls;
using Ink_Canvas_Better.Controls.Panel;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.Windows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var f = IAppHost.GetService<FloatingBar>();
            Temp.Children.Add(f);
            FloatingBarGroup g1 = new FloatingBarGroup();
            var c1 = new CursorControl();
            g1.Add(c1);

            FloatingBarGroup g2 = new FloatingBarGroup();
            var c2 = new MultifuntionControl();
            g2.Add(c2);
            f.Add(g2);
            f.Add(g1);
        }
    }
}
