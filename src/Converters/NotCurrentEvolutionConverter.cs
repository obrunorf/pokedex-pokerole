using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Pokedex.Abstractions;
using Pokedex.Pokerole.Models;

namespace Pokedex.Pokerole.Converters
{
    public class NotCurrentEvolutionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is MainWindowViewModel viewModel)
            {
                if (values[1] is string currentEvolution)
                {
                    return !IsCurrent(currentEvolution, viewModel.selectedPokemon.name);
                }

                if (values[1] is PokemonLocal pokemonLocal)
                {
                    return !IsCurrent(pokemonLocal.name, viewModel.selectedPokemon.name);
                }
            }

            if (values[0] is PokemonLocal selectedPokemon)
            {
                if (values[1] is string currentEvolution)
                {
                    return !IsCurrent(currentEvolution, selectedPokemon.name);
                }

                if (values[1] is PokemonLocal pokemonLocal)
                {
                    return !IsCurrent(pokemonLocal.name, selectedPokemon.name);
                }
            }

            return FontWeights.Normal;
        }

        private static bool IsCurrent(string currentEvolution, string selectedPokemonName)
        {
            return string.Equals(selectedPokemonName, currentEvolution,
                StringComparison.InvariantCultureIgnoreCase);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}