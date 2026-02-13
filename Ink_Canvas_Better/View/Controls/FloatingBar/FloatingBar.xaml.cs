using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar;

namespace Ink_Canvas_Better.View.Controls.FloatingBar;

public partial class FloatingBar : UserControl
{
    public FloatingBarVM Settings => DataContext as FloatingBarVM;

    public FloatingBar()
    {
        InitializeComponent();
        SyncSize();
    }

    private void FloatingBar_Loaded(object sender, RoutedEventArgs e) => Settings.IsInitializing = false;

    private void FloatingBar_SizeChanged(object sender, SizeChangedEventArgs e) => SyncSize();

    private void SyncSize()
    {
        if (Settings is not null)
        {
            this.Settings.Width = this.ActualWidth;
            this.Settings.Height = this.ActualHeight;
        }
    }
}
