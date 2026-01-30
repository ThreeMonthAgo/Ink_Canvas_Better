using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl
{
    /// <summary>
    /// Interaction logic for ClearControl.xaml
    /// </summary>
    public partial class ClearControl : UserControl
    {
        public ClearControl()
        {
            InitializeComponent();
        }

        private void ClearControl_Click(object sender, RoutedEventArgs e)
        {
            IApp.GetService<MainWindow>().ClearStrokes();
        }
    }
}
