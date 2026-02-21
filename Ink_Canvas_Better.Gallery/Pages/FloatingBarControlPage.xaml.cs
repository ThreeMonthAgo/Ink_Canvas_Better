using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Controls.FloatingBarControls;

namespace Ink_Canvas_Better.Gallery.Pages
{
    /// <summary>
    /// Interaction logic for FloatingBarControlPage.xaml
    /// </summary>
    public partial class FloatingBarControlPage : Page
    {
        private readonly FloatingBarControlPageViewModel vm = new();

        public FloatingBarControlPage()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void FloatingBarPopup_Click(object sender, RoutedEventArgs e)
        {
            vm.IsOpen_WithPopup = true;
        }

        private void FloatingBarButton_Click(object sender, RoutedEventArgs e)
        {
            var c = sender as FloatingBarButton;
            c.TextVisibility = c.TextVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }

    public class FloatingBarControlPageViewModel : INotifyPropertyChanged
    {
        private bool isOpen_WithPopup = false;

        public bool IsOpen_WithPopup
        {
            get => isOpen_WithPopup;
            set
            {
                isOpen_WithPopup = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
