using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using Ink_Canvas_Better.Controls;
using Ink_Canvas_Better.Interface;

namespace Ink_Canvas_Better.Services
{
    public class SettingsService
    {
        private Settings? _settings;
        public string SettingsFilePath = "Settings.json";

        public Settings Settings { get { return _settings ??= new(); } }

        public void ReadSettings()
        {
            string path = SettingsFilePath;
            if (File.Exists(path))
            {
                string s = File.ReadAllText(path);
                _settings = JsonSerializer.Deserialize<Settings>(s);
            }
            else
            {
                _settings = new();
                SaveSettings();
            }
        }

        public void SaveSettings()
        {
            string path = SettingsFilePath;
            var f = File.CreateText(path);
            f.Write(JsonSerializer.Serialize(_settings));
            f.Close();
        }
    }

    #region Settings

    /// <summary>
    /// The application settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Version of the application. Synced with Assembly version.
        /// </summary>
        public Version Version = Application.ResourceAssembly.GetName().Version ??= new Version(0, 0, 0, 0);

        /// <summary>
        /// Gets or sets the version of the settings used by the application.
        /// </summary>
        /// <remarks>Use this property to determine compatibility between different settings files or
        /// configurations. Changing the version may affect how settings are interpreted or migrated.</remarks>
        public Version SettingsVersion { get; } = new(2, 0, 0, 0);
    }

    #endregion
}
