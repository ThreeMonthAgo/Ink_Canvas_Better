using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;

namespace Ink_Canvas_Better.Services
{
    internal class SettingsService
    {
        private Settings? _settings;
        public string SettingsFilePath = Path.Combine(App.RootPath, "Settings.json");

        public Settings Settings { get { return _settings ??= new(); } }

        public void ReadSettings(string path)
        {
            if (File.Exists(path))
            {
                string s = File.ReadAllText(path);
                _settings = JsonConvert.DeserializeObject<Settings>(s);
            }
            else
            {
                _settings = new();
                SaveSettings(path);
            }
        }

        public void SaveSettings(string path)
        {
            var f = File.CreateText(path);
            f.Write(JsonConvert.SerializeObject(_settings));
            f.Close();
        }
    }

    #region Settings

    internal class Settings
    {
        [JsonIgnore]
        public Version Version { get; set; } = Application.ResourceAssembly.GetName().Version ??= new Version(0, 0, 0, 0); // if it's 0.0.0.0, something wrong happen

        public Version SettingsVersion { get; set; } = new(2, 0, 0, 0); // The version of settings
    }

    #endregion
}
