using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Data;
using PokeAPI;
using Pokedex.Abstractions;
using Pokedex.Pokerole.Controls;

namespace Pokedex.Pokerole.Converters
{
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

            return new TaskCompletionNotifier<Uri>(Task.FromResult(new Uri("about:blank")));
        }

        private static object BuildImageTask(string pokemonName)
        {
            return new TaskCompletionNotifier<Uri>(DataFetcher.GetNamedApiObject<Pokemon>(pokemonName?.ToLower())
                .ContinueWith(
                    t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                            return new Uri("about:blank");
                        return new Uri(t.Result.Sprites.FrontMale);
                    }));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}