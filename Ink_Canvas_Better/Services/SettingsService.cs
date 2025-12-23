using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using Ink_Canvas_Better.Controls.FloatingBar;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services.JsonConverter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Services
{
    public class SettingsService
    {
        private readonly ILogger<SettingsService> logger;

        private readonly JsonSerializerSettings jsonSerializerSettings = new()
        {
            Converters = [
                new FloartingBarCollectionConverter(),
                new FloatingBarComponentSettingsConverter(),
                new IListConverter(),
            ]
        };

        public string SettingsFilePath = "Settings.json";

        public Settings Settings { get; private set; } = new();

        public SettingsService(ILogger<SettingsService> logger)
        {
            this.logger = logger;

            Settings.PropertyChanged += Settings_PropertyChanged;
        }

        private void Settings_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Settings.Theme):
                    App.GetService<ThemeService>().ChangeTheme(Settings.Theme);
                    break;
                case nameof(Settings.CultureInfo):
                    App.GetService<ThemeService>().ChangeCultureInfo(Settings.CultureInfo);
                    break;
            }
            if (!Settings.IsInitializing)
            {
                App.GetService<SettingsService>().SaveSettings();
            }
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
                    Settings = JsonConvert.DeserializeObject<Settings>(json, jsonSerializerSettings) ?? new();
                    Settings.IsInitializing = false;
                }
                catch (Exception ex)
                {
                    logger.LogWarning($"Load settings failed, creating a new one. {ex.Message}"); // TODO: Perhaps a need to inform the user?
                    ResetSettings();
                }
            }
            else
            {
                logger.LogWarning("Settings file not found, creating a new one.");
                ResetSettings();
            }
        }

        public void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(Settings, jsonSerializerSettings);
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
            Settings.Copy(new Settings());
            SaveSettings();
            logger.LogInformation($"Settings have been restored to defaults");
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
        private ObservableCollection<IFloatingBarComponentSettingBase> _floatingBarCollection = [ CreateDefaultFloatingBar() ];
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
            (floatingBar.Settings as FloatingBarSettings).ScreenIndex = screenindex;
            return floatingBar;
        }
    }

    #endregion
}
