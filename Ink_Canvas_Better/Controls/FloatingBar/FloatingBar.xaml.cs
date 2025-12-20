using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Controls.FloatingBar;
public partial class FloatingBar : UserControl, IFloatingBarComponentSettingBase
{
    public static string Guid { get; } = "D4F5C8A1-6E2B-4F3A-9C1E-2B7D8F9A0B1C";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new FloatingBarSettings();

    public FloatingBar()
    {
        InitializeComponent();

        Loaded += FloatingBar_Loaded;
    }

    public bool TryInvoke() => true;

    private void FloatingBar_Loaded(object sender, RoutedEventArgs e)
    {
        (Settings as FloatingBarSettings).IsInitializing = false;
    }

    public FloatingBar Add(IFloatingBarComponentSettingBase component)
    {
        (Settings as FloatingBarSettings).Items.Add(component);
        return this;
    }
}

public class FloatingBarSettings : INotifyPropertyChanged
{
    private ObservableCollection<IFloatingBarComponentSettingBase>? _items = [];
    private double _spacing = 4.0;
    private Orientation _orientation = Orientation.Horizontal;
    private double _scale = 1.0;

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

    public double Scale
    {
        get { return _scale; }
        set { _scale = value; OnPropertyChanged(); }
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
