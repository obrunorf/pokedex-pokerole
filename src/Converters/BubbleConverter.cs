using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Pokedex.Pokerole.Converters
{
    public class BubbleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is int currentValue && values[1] is int maxValue)
            {
                if (currentValue > 10) { return currentValue.ToString(); } else
                return new string('⚫', currentValue) + new string('◯', Math.Max(maxValue - currentValue,0));

                /* var returnString =
                    new StringBuilder();

                var prefix = values.ElementAtOrDefault(2)?.ToString();

                if (!string.IsNullOrEmpty(prefix))
                    returnString.Append(prefix);

                returnString.Append(Enumerable.Range(0, maxValue).Select(i => "◯").Aggregate((x, y) => $"{x}{y}"));

                foreach (var i in Enumerable.Range(0, currentValue))
                {
                    returnString[i + (prefix?.Length ?? 0)] = '⚫';
                }
                
                return returnString.ToString();*/
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}