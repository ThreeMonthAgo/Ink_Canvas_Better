using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Ink_Canvas_Better.Controls.Helpers.Converters;

public class BooleanToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Visibility v) return false;

        if (v == Visibility.Visible) return true;
        else return false;
    }
}
