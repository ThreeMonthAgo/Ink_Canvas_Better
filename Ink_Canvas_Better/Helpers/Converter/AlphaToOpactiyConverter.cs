using System.Globalization;
using System.Windows.Data;

namespace Ink_Canvas_Better.Helpers.Converter;
public class AlphaToOpactiyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (double)value / 255d;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => (byte)((double)value * 255d);
}
