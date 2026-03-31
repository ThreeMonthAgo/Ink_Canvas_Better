using System.ComponentModel;
using System.Globalization;
using System.Windows;
using Ink_Canvas_Better.Logging;
using Ink_Canvas_Better.Utilities.DataStructures;
using Ink_Canvas_Better.Utilities.Interface;
using Ink_Canvas_Better.View.Windows;
using iNKORE.UI.WPF.Modern;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace Ink_Canvas_Better.Services
{
    public class SettingsService
    {
        private readonly ILogger<SettingsService> logger;
        private readonly IServiceProvider serviceProvider;

        public SettingsService(ILogger<SettingsService> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;

            ChangeCultureInfo(IApp.Settings.CultureInfo);
            ChangeTheme(IApp.Settings.Theme);

            IApp.Settings.PropertyChanged += HandlePropertyChanged;
        }

        public void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IApp.Settings.CultureInfo):
                    ChangeCultureInfo(IApp.Settings.CultureInfo);
                    break;
                case nameof(IApp.Settings.Theme):
                    ChangeTheme(IApp.Settings.Theme);
                    break;
            }
        }

        #region SettingsWindow & LanguageWindow

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

        #region Theme & Language

        public readonly BiDictionary<CultureInfo, string> SupportedLanguage = new()
        {
            { new("en"), "English" },
            { new("zh-CN"), "简体中文" },
            { new("zh-TW"), "繁体中文" },
        };

        public void ChangeCultureInfo(CultureInfo cultureInfo)
        {
            if (SupportedLanguage.ContainsFirst(cultureInfo))
            {
                ChangeLanguage(cultureInfo);
            }
            else
            {
                logger.WriteLog(LogLevel.Warning, () => $"CultureInfo {cultureInfo} is not supported");
                ChangeLanguage(SupportedLanguage.GetFirst(0));
            }
        }

        private void ChangeLanguage(CultureInfo cultureInfo)
        {
            string path = $"Themes/Language/{cultureInfo}.xaml";
            ResourceDictionary newDict = new() { Source = new Uri(path, UriKind.Relative) };
            var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source?.OriginalString.Contains("Language/") == true);
            if (oldDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
            }
            Application.Current.Resources.MergedDictionaries.Add(newDict);
        }

        public void ChangeTheme(int Theme)
        {
            var d = Application.Current.Resources.MergedDictionaries;
            switch (Theme)
            {
                case 0:
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"))
                    {
                        object registryValueObject = key?.GetValue("AppsUseLightTheme");
                        if (registryValueObject is int appsUseLightTheme)
                        {
                            ThemeManager.Current.ApplicationTheme = appsUseLightTheme == 1 ? ApplicationTheme.Light : ApplicationTheme.Dark;
                        }
                        else
                        {
                            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                        }
                    }
                    break;
                case 1:
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                    break;
                case 2:
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                    break;
            }
        }

        #endregion
    }
}
