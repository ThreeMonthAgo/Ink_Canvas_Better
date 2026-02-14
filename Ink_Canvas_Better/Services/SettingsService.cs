using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Logging;
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
            try
            {
                var json = ConfigurationHelper.LoadConfiguration(SettingsFilePath);
                Settings.Copy(JsonConvert.DeserializeObject<Settings>(json, jsonSerializerSettings) ?? new());
                Settings.IsInitializing = false;
            }
            catch (Exception ex)
            {
                logger.WriteLog(LogLevel.Warning, () => $"Load settings failed, creating a new one. {ex.Message}"); // TODO: Perhaps a need to inform the user?
                ResetSettings();
            }
        }

        public void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(Settings, jsonSerializerSettings);
            ConfigurationHelper.SaveConfiguration(json, SettingsFilePath);
        }

        public void ResetSettings()
        {
            Settings.Copy(new Settings());
            SaveSettings();
            logger.WriteLog(LogLevel.Information, "Settings have been restored to defaults");
        }
    }
}
