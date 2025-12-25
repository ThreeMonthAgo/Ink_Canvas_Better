using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Windows;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;

public partial class SettingsControl : UserControl, IFloatingBarComponentSettingBase
{
    private SettingsWindow settingsWindow;

    public static string Guid { get; } = "8AA94A7A-4847-4ED2-930F-292A7BFBA7CB";
    public string ComponentGuid => Guid;
    public object Settings { get; set; } = new SettingsControlSettings();

    public SettingsControl()
    {
        InitializeComponent();

        DataContext = Settings;
        this.Loaded += SettingsControl_Loaded;
        this.MouseUp += SettingsControl_MouseUp;
    }

    public bool TryInvoke() => true;

    private void SettingsControl_Loaded(object sender, RoutedEventArgs e)
    {
        this.settingsWindow = App.GetService<SettingsWindow>();
    }

    private void SettingsControl_MouseUp(object sender, MouseButtonEventArgs e) => settingsWindow.ShowWindow();
}

public class SettingsControlSettings : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (!IsInitializing) App.GetService<SettingsService>().SaveSettings();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [JsonIgnore]
    public bool IsInitializing { get; set; } = true;

    [JsonIgnore]
    public Visibility TextVisibility { get; set; } = Visibility.Collapsed;
}
