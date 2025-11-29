using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Diagnostics;

namespace Ink_Canvas_Better.Controls.Helpers
{
    public static class ThemeHelper
    {
        public static ResourceDictionary Dictionary { get; set; }
        static ThemeHelper()
        {
            Dictionary = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Ink_Canvas_Better.Controls;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute)
            };
        }

        public static T GetFromDictionary<T>(string key)
        {
            if (Dictionary.Contains(key) && Dictionary[key] is T t)
            {
                return t;
            }
            return default;
        }
    }
}
