using System;
using System.Globalization;
using System.Windows.Data;
using Pokedex.Abstractions;

namespace Pokedex.Pokerole.Converters
{
    public class ExpConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {   if (value.Equals(".")) { return "??? "; }
            else
                return $"{value} xp";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}