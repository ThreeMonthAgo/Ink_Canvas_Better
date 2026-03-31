using System.Globalization;
using System.IO;
using System.Windows;
using Ink_Canvas_Better.Utilities.Bases;
using Ink_Canvas_Better.ViewModel.Windows;
using Microsoft.Extensions.Logging;

namespace Ink_Canvas_Better.Model;

/// <summary>
/// The application settings
/// </summary>
public class Settings : ViewModelBase
{
    private Version _appVersion = Application.ResourceAssembly.GetName().Version ??= new Version(0, 0, 0, 0); // 0.0.0.0 => something is wrong
    private Version _settingsVersion = new(2, 0, 0, 0); // Current settings version
    private MainWindowVM _mainWindowVM = new();
    private string _logDirPath = "./Logs/";
    private string? _dataDirPath; // null => C:\Users\<UserName>\AppData\Local\Ink Canvas Better
    private CultureInfo _cultureInfo = new("en");
    private int _theme = 0; // UI theme; 0 => Auto
    private LogLevel _logLevel = LogLevel.Information;

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

    public string DataDirPath
    {
        get
        {
            return _dataDirPath ?? Path.Combine( Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData,
                Environment.SpecialFolderOption.Create),
                "Ink Canvas Better");
        }
        set { SetProperty(ref _dataDirPath, value); }
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

    public LogLevel LogLevel
    {
        get { return _logLevel; }
        set { SetProperty(ref _logLevel, value); }
    }

    #endregion

    public void Copy(Settings settings)
    {
        foreach (var prop in typeof(Settings).GetProperties())
        {
            prop.SetValue(this, prop.GetValue(settings));
        }
    }
}
