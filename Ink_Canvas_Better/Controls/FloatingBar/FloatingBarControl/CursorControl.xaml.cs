using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class CursorControl : UserControl, IFloatingBarComponentSettingBase
{
    private MainWindow mainWindow;

    public static string Guid { get; } = "D034499E-882E-41DF-BE4B-C7446870A93C";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new CursorControlVM();

    public CursorControl()
    {
        InitializeComponent();

        DataContext = Settings;
        this.Loaded += CursorControl_Loaded;
    }

    private void CursorControl_MouseUp(object sender, MouseButtonEventArgs e) => TryInvoke();

    private void CursorControl_Loaded(object sender, RoutedEventArgs e)
    {
        this.mainWindow = App.GetService<MainWindow>();

        (Settings as CursorControlVM).IsInitializing = false;
    }

    public bool TryInvoke()
    {
        mainWindow.CurrentEditingMode = Enums.EditingMode.None;
        return true;
    }
}
