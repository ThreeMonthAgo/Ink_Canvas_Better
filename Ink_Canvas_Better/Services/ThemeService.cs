using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows;
using Ink_Canvas_Better.Utilities.DataStructures;
using iNKORE.UI.WPF.Modern;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;

namespace Ink_Canvas_Better.Services
{
    public class ThemeService(ILogger<ThemeService> logger)
    {
        private readonly ILogger<ThemeService> logger = logger;

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
                logger.LogWarning($"CultureInfo {cultureInfo} is not supported");
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
                            IsChangeToLight(appsUseLightTheme == 1); // 1 -> light; 0 -> dark
                        }
                        else
                        {
                            IsChangeToLight(true);
                        }
                    }
                    break;
                case 1:
                    IsChangeToLight(true);
                    break;
                case 2:
                    IsChangeToLight(false);
                    break;
            }

            void IsChangeToLight(bool b)
            {
                if (b)
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                    d.Remove(new ResourceDictionary() { Source = new Uri("Themes/Image/Dark/FluentUIIcons/FluentUIIcons.xaml", UriKind.Relative) });
                    d.Remove(new ResourceDictionary() { Source = new Uri("Themes/Image/Dark/DrawShapeImageDictionary.xaml", UriKind.Relative) });
                    d.Remove(new ResourceDictionary() { Source = new Uri("Themes/Image/Dark/OthersImageDictionary.xaml", UriKind.Relative) });
                    d.Add(new ResourceDictionary() { Source = new Uri("Themes/Image/Light/FluentUIIcons/FluentUIIcons.xaml", UriKind.Relative) });
                    d.Add(new ResourceDictionary() { Source = new Uri("Themes/Image/Light/DrawShapeImageDictionary.xaml", UriKind.Relative) });
                    d.Add(new ResourceDictionary() { Source = new Uri("Themes/Image/Light/OthersImageDictionary.xaml", UriKind.Relative) });
                }
                else
                {
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                    d.Remove(new ResourceDictionary() { Source = new Uri("Themes/Image/Light/FluentUIIcons/FluentUIIcons.xaml", UriKind.Relative) });
                    d.Remove(new ResourceDictionary() { Source = new Uri("Themes/Image/Light/DrawShapeImageDictionary.xaml", UriKind.Relative) });
                    d.Remove(new ResourceDictionary() { Source = new Uri("Themes/Image/Light/OthersImageDictionary.xaml", UriKind.Relative) });
                    d.Add(new ResourceDictionary() { Source = new Uri("Themes/Image/Dark/FluentUIIcons/FluentUIIcons.xaml", UriKind.Relative) });
                    d.Add(new ResourceDictionary() { Source = new Uri("Themes/Image/Dark/DrawShapeImageDictionary.xaml", UriKind.Relative) });
                    d.Add(new ResourceDictionary() { Source = new Uri("Themes/Image/Dark/OthersImageDictionary.xaml", UriKind.Relative) });
                }
            }
        }
    }
}
