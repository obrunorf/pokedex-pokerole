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
                "Bug" => "#8a970b".ToColorBrush(),
                "Electric" => "#f1bc38".ToColorBrush(),
                "Fire" => "#c22000".ToColorBrush(),
                "Grass" => "#486d3a".ToColorBrush(),
                "Normal" => "#cbc8c1".ToColorBrush(),
                "Rock" => "#927e39".ToColorBrush(),
                "Dark" => "#523c2f".ToColorBrush(),
                "Fairy" => "#cc79d5".ToColorBrush(),
                "Flying" => "#686ec2".ToColorBrush(),
                "Poison" => "#8d418c".ToColorBrush(),
                "Water" => "#2c94f7".ToColorBrush(),
                _ => new SolidColorBrush(Colors.Gray)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}