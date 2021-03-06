﻿using System;
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

            return null;
        }

        private static object BuildImageTask(string pokemonName)
        {
            //var deburger = DataFetcher.GetNamedApiObject<Pokemon>(pokemonName?.ToLower());
            return new TaskCompletionNotifier<Uri>(DataFetcher.GetNamedApiObject<Pokemon>(pokemonName?.ToLower())
                .ContinueWith(
                    t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                            return null;
                        return new Uri(t.Result.Sprites.other.official_artwork?.front_default ?? t.Result.Sprites.FrontMale);
                    }));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}