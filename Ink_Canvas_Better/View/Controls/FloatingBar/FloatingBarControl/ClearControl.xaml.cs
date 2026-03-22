using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl
{
    /// <summary>
    /// Interaction logic for ClearControl.xaml
    /// </summary>
    public partial class ClearControl : UserControl
    {
        public ClearControlVM Settings => DataContext as ClearControlVM;

        public ClearControl()
        {
            InitializeComponent();
        }

        private void ClearControl_Click(object sender, RoutedEventArgs e) => Settings?.Click();
    }
}
