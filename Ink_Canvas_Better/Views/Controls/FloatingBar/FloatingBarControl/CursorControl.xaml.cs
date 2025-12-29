using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Windows;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class CursorControl : UserControl, IFloatingBarComponentSettingBase
{
    private MainWindow mainWindow;

    public object Settings => DataContext as CursorControlVM;

    public CursorControl()
    {
        foreach (DictionaryEntry resource in Application.Current.Resources)
        {
            if (!this.Resources.Contains(resource.Key))
            {
                this.Resources.Add(resource.Key, resource.Value);
            }
        }
        InitializeComponent();

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
        mainWindow.Settings.CurrentEditingMode = Enums.EditingMode.None;
        return true;
    }
}
