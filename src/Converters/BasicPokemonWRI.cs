using System;
using System.Globalization;
using System.Windows.Data;
using Pokedex.Abstractions;

namespace Pokedex.Pokerole.Converters
{
    public abstract class BasicPokemonWRI : IValueConverter
    {
        protected BasicPokemonWRI(WRIType type)
        {
            Type = type;
        }

        private WRIType Type { get; }
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PokemonLocal pokemonLocal)
            {
                return Type switch
                {
                    WRIType.Weakness => pokemonLocal.GetWeakness(),
                    WRIType.Resistance => pokemonLocal.GetResistances(),
                    WRIType.Immunities => pokemonLocal.GetImmunities(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}