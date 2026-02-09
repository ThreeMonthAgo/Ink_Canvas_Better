using System.Collections.ObjectModel;
using System.Windows.Controls;
using Ink_Canvas_Better.Helpers;
using Windows.Win32.Foundation;

namespace Ink_Canvas_Better.View.Pages.Settings.Debug
{
    /// <summary>
    /// Interaction logic for DebugPage.xaml
    /// </summary>
    public partial class DebugPage : Page
    {
        public static ObservableCollection<RECT> Screens => DllHelper.Screens;

        public DebugPage()
        {
            InitializeComponent();

            DataContext = this;
        }
    }
}
