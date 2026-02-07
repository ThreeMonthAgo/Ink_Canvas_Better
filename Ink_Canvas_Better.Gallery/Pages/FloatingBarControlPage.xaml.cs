using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

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

        private void FloatingBarControl_WithPopup_Click(object sender, RoutedEventArgs e)
        {
            vm.IsOpen_WithPopup = true;
        }
    }

    public class FloatingBarControlPageViewModel : INotifyPropertyChanged
    {
        private bool isOpen_WithPopup { get; set; } = false;

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
