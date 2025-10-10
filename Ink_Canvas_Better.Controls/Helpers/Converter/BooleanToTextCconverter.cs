using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ink_Canvas_Better.Controls.Helpers.Converter
{
    [Localizability(LocalizationCategory.NeverLocalize)]
    public sealed class BooleanToTextCconverter_Pin : IValueConverter
    {
        public BooleanToTextCconverter_Pin() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b = value == null ? false : (bool)value;
            return b ? "\ue77a" : "\ue718"; // Pinned : Unpinned
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String s)
            {
                return s == "\ue77a";
            }

            return false;
        }
    }
}
