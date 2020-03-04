using System;
using System.Globalization;
using System.Windows.Data;

namespace Pokedex.Pokerole.Converters
{
    public class BubbleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is int currentValue && values[1] is int maxValue)
            {
                return currentValue > 10
                    ? currentValue.ToString()
                    : new string('⚫', currentValue) + new string('◯', Math.Max(maxValue - currentValue, 0));
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}