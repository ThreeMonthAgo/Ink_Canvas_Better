using System.Globalization;
using System.Windows.Data;

namespace Ink_Canvas_Better.Helpers.Converter;
public class AlphaToOpactiyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            if (value is byte b)
                return b / 255.0;
            if (value is int i)
                return Math.Max(0, Math.Min(1, i / 255.0));
            if (value is double d)
                return Math.Max(0, Math.Min(1, d / 255.0));
            return 1.0;
        }
        catch
        {
            return 1.0;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            if (value is double d)
            {
                d = Math.Max(0, Math.Min(1, d));
                return (byte)Math.Round(d * 255);
            }
            if (value is int i) return (byte)Math.Max(0, Math.Min(255, i));
            return (byte)255;
        }
        catch
        {
            return (byte)255;
        }
    }
}
