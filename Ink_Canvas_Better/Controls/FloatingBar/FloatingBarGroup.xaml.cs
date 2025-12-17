using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
using Ink_Canvas_Better.Services;
using Newtonsoft.Json;

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

        this.Loaded += FloatingBarGroup_Loaded;
    }

    private void FloatingBarGroup_Loaded(object sender, RoutedEventArgs e)
    {
        FloatingBarGroupSettings.IsInitializing = false;
    }

    public bool TryInvoke() => true;

    public FloatingBarGroup Add(IFloatingBarComponentSettingBase component)
    {
        FloatingBarGroupSettings.Items.Add(component);
        return this;
    }
}

public class FloatingBarGroupSettings : INotifyPropertyChanged
{
    private ObservableCollection<IFloatingBarComponentSettingBase>? _items = [];
    private double _spacing = 4.0;
    private Orientation _orientation = Orientation.Vertical;

    #region

    public ObservableCollection<IFloatingBarComponentSettingBase>? Items
    {
        get { return _items; }
        set { _items = value; OnPropertyChanged(); }
    }

    public double Spacing
    {
        get { return _spacing; }
        set { _spacing = value; OnPropertyChanged(); }
    }

    public Orientation Orientation
    {
        get { return _orientation; }
        set { _orientation = value; OnPropertyChanged(); }
    }

    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (!IsInitializing) App.GetService<SettingsService>().SaveSettings();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [JsonIgnore]
    public bool IsInitializing { get; set; } = true;
}