using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Ink_Canvas_Better.Model;
using Ink_Canvas_Better.Services.JsonConverter;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Services
{
    public class SettingsService(ILogger<SettingsService> logger)
    {
        private readonly ILogger<SettingsService> logger = logger;

        private readonly JsonSerializerSettings jsonSerializerSettings = new()
        {
            Converters = [
                new ViewModelBaseConverter(),
                new IListConverter(),
            ]
        };

        public string SettingsFilePath = "Settings.json";

        public Settings Settings { get; private set; } = new();

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
                    Settings.Copy(JsonConvert.DeserializeObject<Settings>(json, jsonSerializerSettings) ?? new());
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
