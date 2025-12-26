using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using Ink_Canvas_Better.Controls.FloatingBar;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.ViewModels;

/// <summary>
/// The application settings
/// </summary>
public class Settings : INotifyPropertyChanged
{
    private Version _appVersion = Application.ResourceAssembly.GetName().Version ??= new Version(0, 0, 0, 0); // 0.0.0.0 => something is wrong
    private Version _settingsVersion = new(2, 0, 0, 0); // Current settings version
    private ObservableCollection<IFloatingBarComponentSettingBase> _floatingBarCollection = [CreateDefaultFloatingBar()];
    private string _logDirPath = "./Logs/";
    private CultureInfo _cultureInfo = new("en");
    private int _theme = 0; // UI theme; 0 => Auto

    #region

    public Version SettingsVersion
    {
        get { return _settingsVersion; }
        set { _settingsVersion = value; OnPropertyChanged(); }
    }
    public Version AppVersion
    {
        get { return _appVersion; }
        set { _appVersion = value; OnPropertyChanged(); }
    }
    public ObservableCollection<IFloatingBarComponentSettingBase> FloatingBarCollection
    {
        get { return _floatingBarCollection; }
        set { _floatingBarCollection = value; OnPropertyChanged(); }
    }
    public string LogDirPath
    {
        get { return _logDirPath; }
        set { _logDirPath = value; OnPropertyChanged(); }
    }

    public CultureInfo CultureInfo
    {
        get { return _cultureInfo; }
        set { _cultureInfo = value; OnPropertyChanged(); }
    }

    public int Theme
    {
        get { return _theme; }
        set { _theme = value; OnPropertyChanged(); }
    }

    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    [JsonIgnore]
    public bool IsInitializing { get; set; } = true;

    public void Copy(Settings settings)
    {
        foreach (var prop in this.GetType().GetProperties())
        {
            prop.SetValue(this, prop.GetValue(settings));
        }
    }

    public static FloatingBar CreateDefaultFloatingBar(int screenindex = 0)
    {
        var floatingBar =
            App.GetService<FloatingBar>()
                .Add(App.GetService<FloatingBarGroup>()
                    .Add(App.GetService<MultifunctionControl>())
                )
                .Add(App.GetService<FloatingBarGroup>()
                    .Add(App.GetService<CursorControl>())
                    .Add(App.GetService<PenControl>())
                )
                .Add(App.GetService<FloatingBarGroup>()
                    .Add(App.GetService<SettingsControl>())
                );
        (floatingBar.Settings as FloatingBarVM).ScreenIndex = screenindex;
        return floatingBar;
    }
}
