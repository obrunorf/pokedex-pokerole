using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media;

namespace Pokedex.Pokerole.Converters
{
    public class CategoryForegroundColorConverter : IValueConverter
    {
        private static string[] White { get; } = {"Grass", "Fire", "Poison", "Flying", "Water"};

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (White.Contains(value))
            {
                return new SolidColorBrush(Colors.WhiteSmoke);
            }

            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}