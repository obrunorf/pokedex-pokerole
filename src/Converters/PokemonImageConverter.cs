using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using PokeAPI;
using Pokedex.Abstractions;
using Pokedex.Pokerole.Controls;
using Pokedex.Pokerole.Models;

namespace Pokedex.Pokerole.Converters
{
    public class CurrentEvolutionConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is MainWindowViewModel viewModel)
            {
                if (values[1] is string currentEvolution)
                {
                    return GetFontWeight(currentEvolution, viewModel.selectedPokemon.name);
                }

                if (values[1] is PokemonLocal pokemonLocal)
                {
                    return GetFontWeight(pokemonLocal.name, viewModel.selectedPokemon.name);
                }
            }

            if (values[0] is PokemonLocal selectedPokemon)
            {
                if (values[1] is string currentEvolution)
                {
                    return GetFontWeight(currentEvolution, selectedPokemon.name);
                }

                if (values[1] is PokemonLocal pokemonLocal)
                {
                    return GetFontWeight(pokemonLocal.name, selectedPokemon.name);
                }
            }

            return null;
        }

        private static object GetFontWeight(string currentEvolution, string selectedPokemonName)
        {
            return string.Equals(selectedPokemonName, currentEvolution,
                StringComparison.InvariantCultureIgnoreCase)
                ? FontWeights.Bold
                : (object) null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PokemonImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string pokemonName)
            {
                return BuildImageTask(pokemonName);
            }

            if (value is PokemonLocal pokemonLocal)
            {
                return BuildImageTask(pokemonLocal.name);
            }

            return null;
        }

        private static object BuildImageTask(string pokemonName)
        {
            return new TaskCompletionNotifier<Uri>(DataFetcher.GetNamedApiObject<Pokemon>(pokemonName?.ToLower())
                .ContinueWith(
                    t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                            return null;
                        else
                            return new Uri(t.Result.Sprites.FrontMale);
                    }));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}