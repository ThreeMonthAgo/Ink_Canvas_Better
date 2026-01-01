using System;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar;

namespace Ink_Canvas_Better.View.Controls.FloatingBar;

public partial class FloatingBarGroup : UserControl
{
    public FloatingBarGroupVM Settings => DataContext as FloatingBarGroupVM;

    public FloatingBarGroup()
    {
        InitializeComponent();

        this.Loaded += FloatingBarGroup_Loaded;
    }

    private void FloatingBarGroup_Loaded(object sender, RoutedEventArgs e)
    {
        Settings.IsInitializing = false;
    }
}
