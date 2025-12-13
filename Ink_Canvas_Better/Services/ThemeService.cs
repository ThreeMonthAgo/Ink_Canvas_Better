using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using Ink_Canvas_Better.Utilities.DataStructures;
using Microsoft.Extensions.Logging;

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
            ResourceDictionary newDict;
            newDict = new ResourceDictionary { Source = new Uri(path, UriKind.Relative) };
            var oldDict = Application.Current.Resources.MergedDictionaries.FirstOrDefault(d => d.Source?.OriginalString.Contains("Languages/") == true);
            if (oldDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
            }
            Application.Current.Resources.MergedDictionaries.Add(newDict);
        }
    }
}
