﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace Pokedex.Pokerole
{
    public class BubbleConverterHP : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int i)
            {
                return new String('⚫', i);
            }
            else return null;
          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}