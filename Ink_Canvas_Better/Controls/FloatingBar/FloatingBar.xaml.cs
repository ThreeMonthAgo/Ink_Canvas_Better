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

    #region Properties

    #region Orientation

    public Orientation Orientation
    {
        get { return (Orientation)GetValue(OrientationProperty); }
        set { SetValue(OrientationProperty, value); }
    }

    public static readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(FloatingBar), new PropertyMetadata(Orientation.Horizontal));

    #endregion

    #region Spacing

    public double Spacing
    {
        get { return (double)GetValue(SpacingProperty); }
        set { SetValue(SpacingProperty, value); }
    }

    public static readonly DependencyProperty SpacingProperty =
        DependencyProperty.Register(nameof(Spacing), typeof(double), typeof(FloatingBar), new PropertyMetadata(4.0));

    #endregion

    #endregion

}

public class FloatingBarSettings
{
    public ObservableCollection<IFloatingBarComponentSettingBase>? Items { get; set; } = [];
}
