using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ink_Canvas_Better.Controls.FloatingBar;
public partial class FloatingBar : UserControl, IFloatingBarComponentSettingBase
{
    public static string Guid { get; } = "D4F5C8A1-6E2B-4F3A-9C1E-2B7D8F9A0B1C";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new FloatingBarSettings();
    public FloatingBarSettings FloatingBarSettings => Settings as FloatingBarSettings;

    public FloatingBar()
    {
        InitializeComponent();

        Loaded += FloatingBar_Loaded;
    }

    private void FloatingBar_Loaded(object sender, RoutedEventArgs e)
    {
        this.RenderTransform = new TranslateTransform();
    }

    public FloatingBar Add(IFloatingBarComponentSettingBase component)
    {
        FloatingBarSettings.Items.Add(component);
        return this;
    }
}

public class FloatingBarSettings
{
    public ObservableCollection<IFloatingBarComponentSettingBase>? Items { get; set; } = [];
    public double Spacing { get; set; } = 4.0;
    public Orientation Orientation { get; set; } = Orientation.Horizontal;
}
