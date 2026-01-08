using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModel.Controls.FloatingBar.FloatingBarControl;
using static Ink_Canvas_Better.Utilities.Enums.InkCanvas;
using Ink_Canvas_Better.Services;

namespace Ink_Canvas_Better.View.Controls.FloatingBar.FloatingBarControl;

public partial class CursorControl : UserControl
{
    public CursorControlVM Settings => DataContext as CursorControlVM;

    public CursorControl()
    {
        InitializeComponent();

        this.Loaded += CursorControl_Loaded;
    }

    private void CursorControl_MouseUp(object sender, MouseButtonEventArgs e) => Apply();

    private void CursorControl_Loaded(object sender, RoutedEventArgs e)
    {
        Settings.IsInitializing = false;
    }

    public bool Apply()
    {
        IApp.GetService<SettingsService>().Settings.MainWindowVM.CurrentEditingMode = EditingMode.None;
        return true;
    }
}
