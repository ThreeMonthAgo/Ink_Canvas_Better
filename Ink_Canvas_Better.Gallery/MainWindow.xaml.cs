using System.Windows;
using System.Windows.Controls;

namespace ICBUITest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var c = sender as MenuItem;
            MainFrame.Navigate(new Uri($"Pages/{(string)(c.Header)}Page.xaml", UriKind.Relative));
        }
    }
}