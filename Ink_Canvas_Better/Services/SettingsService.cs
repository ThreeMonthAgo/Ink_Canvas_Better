using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Ink_Canvas_Better.Controls.FloatingBar;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Interface;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Services
{
    public class SettingsService
    {
        private readonly ILogger<SettingsService> logger;
        private readonly ControlsService controlsService;
        private readonly ThemeService themeService;

        public string SettingsFilePath = "Settings.json";

        public Settings Settings { get; private set; } = new();

        public SettingsService(ILogger<SettingsService> logger, ControlsService controlsService, ThemeService themeService)
        {
            this.logger = logger;
            this.controlsService = controlsService;
            this.themeService = themeService;

            LoadSettings();
        }

        public void LoadSettings()
        {
            if (File.Exists(SettingsFilePath))
            {
                try
                {
                    using var stream = new FileStream(
                        SettingsFilePath,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite
                    );
                    using var reader = new StreamReader(stream);
                    var json = reader.ReadToEnd();
                    Settings = JsonConvert.DeserializeObject<Settings>(json, controlsService) ?? new();
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Load settings failed, creating a new one. {ex.Message}"); // TODO: Perhaps a need to inform the user?
                    Settings = new();
                    SaveSettings();
                }
            }
            else
            {
                logger.LogWarning("Settings file not found, creating a new one.");
                Settings = new();
                SaveSettings();
            }
        }

        public void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(Settings, controlsService);
            using var stream = new FileStream(
                SettingsFilePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.ReadWrite
            );
            using var writer = new StreamWriter(stream);
            writer.Write(json);
        }

        public void ResetSettings()
        {
            throw new NotImplementedException();// TODO
        }
    }

    #region Settings

    /// <summary>
    /// The application settings
    /// </summary>
    public class Settings : INotifyPropertyChanged
    {
        private Version _appVersion = Application.ResourceAssembly.GetName().Version ??= new Version(0, 0, 0, 0); // 0.0.0.0 => something is wrong
        private Version _settingsVersion = new(2, 0, 0, 0); // Current settings version
        private ObservableCollection<IFloatingBarComponentSettingBase> _floatingBarCollection = [
                Program.GetService<FloatingBar>()
                    .Add(Program.GetService<FloatingBarGroup>()
                            .Add(Program.GetService<MultifunctionControl>())
                            .Add(Program.GetService<SettingsControl>())
                    )
            ];
        private string _logDirPath = "./Logs/";
        private CultureInfo _cultureInfo = new CultureInfo("en");

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
            set { _cultureInfo = value; Program.GetService<ThemeService>().ChangeCultureInfo(CultureInfo); OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    #endregion
}
