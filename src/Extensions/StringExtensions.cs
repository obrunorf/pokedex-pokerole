using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Media;

namespace Pokedex.Pokerole.Extensions
{
    internal static class StringExtensions
    {
        public static string GetReadableFileName(this string fullFileName)
        {
            var fileName = Path.GetFileNameWithoutExtension(fullFileName);

            if (fileName != null && fileName.Contains("."))
            {
                fileName = fileName.Split('.').Last();
            }

            return fileName != null
                ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fileName.ToLower())
                : string.Empty;
        }

        public static SolidColorBrush ToColorBrush(this string hex)
        {
            return (SolidColorBrush) new BrushConverter().ConvertFrom(hex);
        }
    }
}