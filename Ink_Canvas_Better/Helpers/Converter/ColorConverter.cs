using System.Windows.Media;

namespace Ink_Canvas_Better.Helpers.Converter
{
    static class ColorConverter
    {
        public static Color HexToColor(string hex)
        {
            if (hex.StartsWith('#')) hex = hex[1..];
            byte a, r, g, b;
            if (hex.Length == 6)
            {
                a = 0xFF;
                r = Convert.ToByte(hex[..2], 16);
                g = Convert.ToByte(hex[2..4], 16);
                b = Convert.ToByte(hex[4..6], 16);
            }
            else
            {
                a = Convert.ToByte(hex[..2], 16);
                r = Convert.ToByte(hex[2..4], 16);
                g = Convert.ToByte(hex[4..6], 16);
                b = Convert.ToByte(hex[6..8], 16);
            }
            return Color.FromArgb(a, r, g, b);
        }

        public static SolidColorBrush HexToSolidColorBrush(string hex) => new(HexToColor(hex));

        public static string ColorToHex(Color color)
        {
            return $"{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}
