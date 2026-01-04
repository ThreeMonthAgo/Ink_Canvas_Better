using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows;
using Ink_Canvas_Better.Services;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.ViewModel.Windows;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Model;

/// <summary>
/// The application settings
/// </summary>
public class Settings
{
    private Version _appVersion = Application.ResourceAssembly.GetName().Version ??= new Version(0, 0, 0, 0); // 0.0.0.0 => something is wrong
    private Version _settingsVersion = new(2, 0, 0, 0); // Current settings version
    private MainWindowVM _mainWindowVM = new();
    private string _logDirPath = "./Logs/";
    private CultureInfo _cultureInfo = new("en");
    private int _theme = 0; // UI theme; 0 => Auto

    public Settings()
    {
        this.PropertyChanged += Settings_PropertyChanged;
    }

    private void Settings_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(Theme):
                IApp.GetService<ThemeService>().ChangeTheme(Theme);
                break;
            case nameof(CultureInfo):
                IApp.GetService<ThemeService>().ChangeCultureInfo(CultureInfo);
                break;
        }
    }

    #region

    public Version SettingsVersion
    {
        get { return _settingsVersion; }
        set { SetProperty(ref _settingsVersion, value); }
    }

    public Version AppVersion
    {
        get { return _appVersion; }
        set { SetProperty(ref _appVersion, value); }
    }

    public MainWindowVM MainWindowVM
    {
        get { return _mainWindowVM; }
        set { SetProperty(ref _mainWindowVM, value); }
    }

    public string LogDirPath
    {
        get { return _logDirPath; }
        set { SetProperty(ref _logDirPath, value); }
    }

    public CultureInfo CultureInfo
    {
        get { return _cultureInfo; }
        set { SetProperty(ref _cultureInfo, value); }
    }

    public int Theme
    {
        get { return _theme; }
        set { SetProperty(ref _theme, value); }
    }

    #endregion

    protected virtual void SetProperty<T>(
        ref T field,
        T newValue,
        [CallerMemberName] string? propertyName = null,
        bool force = true)
    {
        if (EqualityComparer<T>.Default.Equals(field, newValue))
        {
            if (force) OnPropertyChanged(propertyName);
        }
        else
        {
            field = newValue;
            OnPropertyChanged(propertyName);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null, bool force = true)
    {
        Debug.WriteLine(MainWindowVM.GetHashCode()); // wrong here: reference changed. see MainWindowVM
        if (!IsInitializing) IApp.GetService<SettingsService>().SaveSettings();
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [JsonIgnore]
    public bool IsInitializing { get; set; } = true;

    public void Copy(Settings settings)
    {
        foreach (var prop in typeof(Settings).GetProperties())
        {
            prop.SetValue(this, prop.GetValue(settings));
        }
    }
}
