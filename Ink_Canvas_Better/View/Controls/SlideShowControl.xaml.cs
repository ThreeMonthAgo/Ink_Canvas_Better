using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.ViewModel.Controls;

namespace Ink_Canvas_Better.View.Controls
{
    /// <summary>
    /// Interaction logic for SlideShowControl.xaml
    /// </summary>
    public partial class SlideShowControl : UserControl
    {
        public SlideShowControlVM Settings => DataContext as SlideShowControlVM;

        public SlideShowControl()
        {
            InitializeComponent();
            SyncSize();
        }

        private void ToolBarControl_Loaded(object sender, RoutedEventArgs e) => Settings.IsInitializing = false;

        private void ToolBarControl_SizeChanged(object sender, SizeChangedEventArgs e) => SyncSize();

        private void SyncSize()
        {
            if (Settings is not null)
            {
                this.Settings.Width = this.ActualWidth;
                this.Settings.Height = this.ActualHeight;
            }
        }
    }
}
