using System;
using System.Globalization;
using System.Windows.Data;
using Pokedex.Abstractions;

namespace Pokedex.Pokerole.Converters
{
    public class PokemonTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PokemonLocal pokemonLocal)
            {
                return $"{pokemonLocal.number} - {pokemonLocal.name}";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}