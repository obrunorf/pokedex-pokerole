using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Data;
using System.Windows.Threading;
using Newtonsoft.Json;
using PokeAPI;
using Pokedex.Abstractions;
using Pokedex.Pokerole.Extensions;
using PropertyChanged;

namespace Pokedex.Pokerole.Models
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel
    {
        private string _searchText;
        private PokemonLocal _selectedPokemon;

        public MainWindowViewModel(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            var moveDetails = LoadMoves(Assembly.GetExecutingAssembly().GetFileStream("moves.json")).ToDictionary(i => i.name);


            Pokemons = LoadPokeData(Assembly.GetExecutingAssembly().GetFileStream("pokemon.json")).Select(i =>
            {
                foreach (var move in i.moves)
                {
                    move.Detail = moveDetails.TryGetValue(move.name, out var detail) ? detail : null;
                }

                return i;
            }).ToList();
            FilteredPokemons = CollectionViewSource.GetDefaultView(Pokemons);
        }

        public Dispatcher Dispatcher { get; }

        private CancellationTokenSource ImageCancellationTokenSource { get; set; }

        public List<PokemonLocal> Pokemons { get; set; }

        public ICollectionView FilteredPokemons { get; set; }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                if (string.IsNullOrEmpty(value))
                    FilteredPokemons.Filter = null;
                else
                    FilteredPokemons.Filter = o =>
                        o is PokemonLocal pokemonLocal &&
                        pokemonLocal.name.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);
            }
        }


        public PokemonLocal SelectedPokemon
        {
            get => _selectedPokemon;
            set
            {
                _selectedPokemon = value;
                PokemonImage = null;
                ImageCancellationTokenSource?.Cancel();

                if (value?.number != null)
                {
                    ImageCancellationTokenSource = new CancellationTokenSource();
                    DataFetcher.GetApiObject<Pokemon>(value.number.Value).ContinueWith(
                        t => { Dispatcher.Invoke(() => { PokemonImage = new Uri(t.Result.Sprites.FrontMale); }); },
                        ImageCancellationTokenSource.Token);
                }
            }
        }

        public Uri PokemonImage { get; set; }

        private static List<MoveDetail> LoadMoves(Stream fileStream) //
        {
            using var r = new StreamReader(fileStream);
            var json = r.ReadToEnd();
            var moves = JsonConvert.DeserializeObject<List<MoveDetail>>(json);
            return moves;
        }

        private static List<PokemonLocal> LoadPokeData(Stream fileStream) //todo :  nao adicionar pokemons mega
        {
            using var r = new StreamReader(fileStream);
            var json = r.ReadToEnd();

            var pkmns = JsonConvert.DeserializeObject<List<PokemonLocal>>(json);
            //ate aqui ok preenchemos nosso array com a lista do jasao e os argonautas
            for (var i = 0; i < pkmns.Count; i++)
            {
                if (pkmns[i].name.Contains("("))
                {
                    pkmns.Remove(pkmns[i]);
                    i--;
                }

                ;
            }

            return pkmns;
        }
    }
}