using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModel.Controls;

namespace Ink_Canvas_Better.View.Controls
{
    /// <summary>
    /// Interaction logic for ToolBarControl.xaml
    /// </summary>
    public partial class ToolBarControl : UserControl
    {
        public ToolBarControlVM Settings => DataContext as ToolBarControlVM;

        public ToolBarControl()
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
