using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using Ink_Canvas_Better.Model;
using Ink_Canvas_Better.Services.JsonConverter;
using Ink_Canvas_Better.Utilities.Attributes;
using Ink_Canvas_Better.Utilities.Interface;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Helpers
{
    public static class SettingsHelper
    {
        private static readonly JsonSerializerSettings jsonSerializerSettings = new()
        {
            Converters = [
                new FloatingBarViewModelBaseConverter(),
                new IListConverter(),
            ]
        };

        public static readonly string SettingsFilePath = "Settings.json";


        /// <remarks>
        /// <para>string: guid</para>
        /// <para>Type1: viewmodel type</para>
        /// <para>Type2: view type</para>
        /// </remarks>
        public static ConcurrentDictionary<string, (Type, Type)> RegisteredComponents { get; } = [];

        public static void LoadSettings()
        {
            try
            {
                var json = ConfigurationHelper.LoadConfiguration(SettingsFilePath);
                IApp.Settings.Copy(JsonConvert.DeserializeObject<Settings>(json, jsonSerializerSettings) ?? new());
                IApp.Settings.IsInitializing = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Load settings failed, creating a new one. {ex.Message}"); // TODO: Perhaps a need to inform the user?
                ResetSettings();
                // TODO: Log the error
            }
        }

        public static void SaveSettings()
        {
            var json = JsonConvert.SerializeObject(IApp.Settings, jsonSerializerSettings);
            ConfigurationHelper.SaveConfiguration(json, SettingsFilePath);
        }

        public static void ResetSettings()
        {
            IApp.Settings.Copy(new Settings());
            SaveSettings();
            Debug.WriteLine("Settings have been restored to defaults");
            // TODO: Log the error
        }

        /// <summary>
        /// registers all components marked with the ComponentAttribute in the current AppDomain assemblies.
        /// </summary>
        public static void DetectAndRegisterComponents()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblies)
            {
                var viewModelTypes = assembly.GetTypes().Where(t => t.GetCustomAttribute<ComponentAttribute>() != null);
                foreach (var viewModelType in viewModelTypes)
                {
                    var componentAttribute = viewModelType.GetCustomAttribute<ComponentAttribute>();
                    if (componentAttribute != null)
                    {
                        var viewType = componentAttribute.ViewType;
                        var guid = componentAttribute.Guid;
                        var r = RegisteredComponents.TryAdd(guid, (viewModelType, viewType));
                        if (r)
                        {
                            DataTemplateHelper.RegisterDataTemplate(viewModelType, viewType);
                        }
                        else
                        {
                            Debug.WriteLine($"Component with guid {guid} has already registered.");
                        }
                    }
                }
            }
        }
    }
}
