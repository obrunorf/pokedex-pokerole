using System.Windows.Media;

namespace Pokedex.Pokerole.Extensions
{
    internal static class StringExtensions
    {
        public static SolidColorBrush ToColorBrush(this string hex)
        {
            return (SolidColorBrush) new BrushConverter().ConvertFrom(hex);
        }
    }
}