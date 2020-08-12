using System;
using System.Globalization;
using System.Windows.Data;
using Pokedex.Abstractions;

namespace Pokedex.Pokerole.Converters
{
    public class MoveDamageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is string damage && values[1] is string modifier)
            {
                if (damage.Equals("-")){
                    return "Support";
                }else
                return modifier.Equals("Fighting")
                    ? damage+" + Strength"                    
                        : damage+" + "+modifier;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}