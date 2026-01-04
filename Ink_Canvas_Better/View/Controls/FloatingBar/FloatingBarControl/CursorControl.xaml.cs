using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.View.Windows;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class CursorControl : UserControl
{
    private MainWindow mainWindow;

    public CursorControlVM Settings => DataContext as CursorControlVM;

    public CursorControl()
    {
        InitializeComponent();

        this.Loaded += CursorControl_Loaded;
    }

    private void CursorControl_MouseUp(object sender, MouseButtonEventArgs e) => Apply();

    private void CursorControl_Loaded(object sender, RoutedEventArgs e)
    {
        this.mainWindow = IApp.GetService<MainWindow>();
        Settings.IsInitializing = false;
    }

    public bool Apply()
    {
        mainWindow.Settings.CurrentEditingMode = EditingMode.None;
        return true;
    }
}
