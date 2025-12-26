using System;
using System.Windows;
using System.Windows.Controls;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar;

namespace Ink_Canvas_Better.Controls.FloatingBar;

public partial class FloatingBarGroup : UserControl, IFloatingBarComponentSettingBase
{
    public static string Guid { get; } = "B1E2F3A4-5678-90AB-CDEF-1234567890AB";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new FloatingBarGroupVM();

    public FloatingBarGroup()
    {
        InitializeComponent();

        DataContext = Settings;
        this.Loaded += FloatingBarGroup_Loaded;
    }

    private void FloatingBarGroup_Loaded(object sender, RoutedEventArgs e)
    {
        (Settings as FloatingBarGroupVM).IsInitializing = false;
    }

    public bool TryInvoke() => true;

    public FloatingBarGroup Add(IFloatingBarComponentSettingBase component)
    {
        (Settings as FloatingBarGroupVM).Items.Add(component);
        return this;
    }
}
