using System;
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

        Loaded += FloatingBar_Loaded;
    }

    private void FloatingBar_Loaded(object sender, RoutedEventArgs e)
    {
        Settings.IsInitializing = false;
    }
}
