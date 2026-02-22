using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;

namespace Ink_Canvas_Better.View.Controls
{
    /// <summary>
    /// Interaction logic for SlideShowControl.xaml
    /// </summary>
    public partial class SlideShowControl : UserControl
    {
        public SlideShowControl() => InitializeComponent();

        private void FloatingBarButton_Previous_Click(object sender, RoutedEventArgs e) =>
            IApp.GetService<PPTService>().Previous();

        private void FloatingBarButton_Next_Click(object sender, RoutedEventArgs e) =>
            IApp.GetService<PPTService>().Next();
    }
}
