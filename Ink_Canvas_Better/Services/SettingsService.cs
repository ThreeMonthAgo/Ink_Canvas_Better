using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using Ink_Canvas_Better.Controls.FloatingBar;
using Ink_Canvas_Better.Controls.FloatingBar.FloatingBarControl;
using Ink_Canvas_Better.Interface;
using Ink_Canvas_Better.Services.JsonConverter;
using Ink_Canvas_Better.ViewModels;
using Ink_Canvas_Better.ViewModels.Controls.FloatingBar;
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
}
