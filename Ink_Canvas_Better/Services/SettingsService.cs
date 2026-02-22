using Ink_Canvas_Better.Helpers;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Model;
using Ink_Canvas_Better.Services.JsonConverter;
using Ink_Canvas_Better.View.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Services
{
    public class SettingsService(ILogger<SettingsService> logger, IServiceProvider serviceProvider)
    {
        private readonly ILogger<SettingsService> logger = logger;
        private readonly IServiceProvider serviceProvider = serviceProvider;

        #region Serialization & Deserialization

        private readonly JsonSerializerSettings jsonSerializerSettings = new()
        {
            Converters = [
                new FloatingBarViewModelBaseConverter(),
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

        #endregion

        #region Windows

        public SettingsWindow SettingsWindow { get; private set; }

        public void ShowSettingsWindow()
        {
            if (SettingsWindow is null || !SettingsWindow.IsLoaded)
            {
                SettingsWindow = ActivatorUtilities.CreateInstance<SettingsWindow>(serviceProvider);
                SettingsWindow.Show();
            }
            else
            {
                SettingsWindow.Activate();
            }
        }

        public LanguageWindow LanguageWindow { get; private set; }

        public void ShowLanguageWindow()
        {
            if (LanguageWindow is null || !LanguageWindow.IsLoaded)
            {
                LanguageWindow = ActivatorUtilities.CreateInstance<LanguageWindow>(serviceProvider);
                LanguageWindow.ShowDialog();
            }
            else
            {
                LanguageWindow.Activate();
            }
        }

        #endregion
    }
}
