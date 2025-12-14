using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ink_Canvas_Better.Interface;

namespace Ink_Canvas_Better.Controls.FloatingBar;
/// <summary>
/// FloatingBarGroup.xaml 的交互逻辑
/// </summary>
public partial class FloatingBarGroup : UserControl, IFloatingBarComponentSettingBase
{
    public static string Guid { get; } = "B1E2F3A4-5678-90AB-CDEF-1234567890AB";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new FloatingBarGroupSettings();
    public FloatingBarGroupSettings FloatingBarGroupSettings => Settings as FloatingBarGroupSettings;

    public FloatingBarGroup()
    {
        InitializeComponent();
    }

    public FloatingBarGroup Add(IFloatingBarComponentSettingBase component)
    {
        FloatingBarGroupSettings.Items.Add(component);
        return this;
    }
}

public class FloatingBarGroupSettings
{
    public ObservableCollection<IFloatingBarComponentSettingBase>? Items { get; set; } = [];
    public double Spacing { get; set; } = 4.0;
    public Orientation Orientation { get; set; } = Orientation.Vertical;
}