using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Pokedex.Pokerole.Extensions;

namespace Pokedex.Pokerole.Converters
{
    public class CategoryBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                "Bug" => "#A8B820".ToColorBrush(),
                "Electric" => "#F8D030".ToColorBrush(),
                "Fire" => "#F08030".ToColorBrush(),
                "Grass" => "#78C850".ToColorBrush(),
                "Normal" => "#A8A878".ToColorBrush(),
                "Rock" => "#B8A038".ToColorBrush(),
                "Dark" => "#705848".ToColorBrush(),
                "Fairy" => "#EE99AC".ToColorBrush(),
                "Flying" => "#A890F0".ToColorBrush(),
                "Poison" => "#A040A0".ToColorBrush(),
                "Water" => "#6890F0".ToColorBrush(),
                "Ice" => "#98D8D8".ToColorBrush(),
                "Fighting" => "#C03028".ToColorBrush(),                
                "Ground" => "#E0C068".ToColorBrush(),
                "Psychic" => "#F85888".ToColorBrush(),
                "Dragon" => "#7038F8".ToColorBrush(),
                "Ghost" => "#705898".ToColorBrush(),                
                "Steel" => "#B8B8D0".ToColorBrush(),
                _ => new SolidColorBrush(Colors.Gray)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}