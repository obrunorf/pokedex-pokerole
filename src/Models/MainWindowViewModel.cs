using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.Windows.Input;
using Newtonsoft.Json;
using Pokedex.Abstractions;
using Pokedex.Pokerole.Controls;
using Pokedex.Pokerole.Extensions;
using PropertyChanged;

namespace Pokedex.Pokerole.Models
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel
    {
        private const string SettingsFileName = "settings.json";
        private readonly Dictionary<string, AbilityDetail> _abilityDetails;
        private readonly Assembly _executingAssembly;
        private readonly Dictionary<string, MoveDetail> _moveDetails;
        private string _searchText;

        public MainWindowViewModel()
        {
            _executingAssembly = Assembly.GetExecutingAssembly();

            selectableJsonFiles = _executingAssembly.GetFileNames().Where(i => i.Contains("_")).Select(i =>
                new SelectableJsonFile
                {
                    fullName = i,
                    displayName = i.GetReadableFileName()
                }).ToList();

            _moveDetails = LoadMoves(_executingAssembly.GetFileStream("moves.json"))
                .ToDictionary(i => i.name);
            _abilityDetails = LoadAbilities(_executingAssembly.GetFileStream("abilities.json"))
                .ToDictionary(i => i.name);
            changePokemonCommand = new CommandHandler((x) =>
            {
                var newPokemon = pokemons.SingleOrDefault(o => string.Equals(o.name, x?.ToString(), StringComparison.InvariantCultureIgnoreCase));

                if (newPokemon != null && selectedPokemon != newPokemon)
                {
                    selectedPokemon = newPokemon;
                }
            }, (x) => true);

            currentSettings = LoadSettings() ?? new Settings {selectedFile = selectableJsonFiles.First()};
            currentSettings.PropertyChanged += CurrentSettingsOnPropertyChanged;
            SaveSettings(currentSettings);
            LoadPokemons();
        }

        public List<SelectableJsonFile> selectableJsonFiles { get; }

        public Settings currentSettings { get; }

        private List<PokemonLocal> pokemons { get; set; }

        public ICommand changePokemonCommand { get; }

        public ICollectionView filteredPokemons { get; set; }

        public string searchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                ApplyFilter();
            }
        }

        private void ApplyFilter()
        {
            if (string.IsNullOrEmpty(searchText))
                filteredPokemons.Filter = null;
            else
                filteredPokemons.Filter = o =>
                    o is PokemonLocal pokemonLocal &&
                    (pokemonLocal.name.StartsWith(searchText, StringComparison.InvariantCultureIgnoreCase)
                     || pokemonLocal.number.ToString().Equals(searchText, StringComparison.InvariantCultureIgnoreCase));
        }


        public PokemonLocal selectedPokemon { get; set; }

        private void LoadPokemons()
        {
            pokemons = LoadPokeData(_executingAssembly.GetFileStream(currentSettings.selectedFile.fullName)).Select(i =>
            {
                foreach (var move in i.moves)
                    move.Detail = _moveDetails.TryGetValue(move.name, out var detail) ? detail : null;

                foreach (var ability in i.abilities.Where(_abilityDetails.ContainsKey))
                    i.abilitiesDetailed.Add(_abilityDetails[ability]);

                return i;
            }).ToList();

            filteredPokemons = CollectionViewSource.GetDefaultView(pokemons);
            ApplyFilter();
        }

        private void CurrentSettingsOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveSettings(currentSettings);
            LoadPokemons();
        }

        private Settings LoadSettings()
        {
            var file = new FileInfo(SettingsFileName);

            if (file.Exists)
            {
                using var fileStream = file.OpenRead();
                using var streamReader = new StreamReader(fileStream);
                var fileContent = streamReader.ReadToEnd();
                var deserializeObject = JsonConvert.DeserializeObject<Settings>(fileContent);

                if (selectableJsonFiles.All(i =>
                    !string.Equals(i.fullName, deserializeObject.selectedFile.fullName,
                        StringComparison.InvariantCultureIgnoreCase)))
                {
                    return null;
                }

                return deserializeObject;
            }

            return null;
        }

        private static void SaveSettings(Settings settings)
        {
            var file = new FileInfo(SettingsFileName);
            var serializeObject = JsonConvert.SerializeObject(settings);
            File.WriteAllText(file.FullName, serializeObject);
        }

        private static List<MoveDetail> LoadMoves(Stream fileStream) //
        {
            using var r = new StreamReader(fileStream);
            var json = r.ReadToEnd();
            var moves = JsonConvert.DeserializeObject<List<MoveDetail>>(json);
            return moves;
        }

        private static List<AbilityDetail> LoadAbilities(Stream fileStream) //
        {
            using var r = new StreamReader(fileStream);
            var json = r.ReadToEnd();
            var abilities = JsonConvert.DeserializeObject<List<AbilityDetail>>(json);
            return abilities;
        }

        private static List<PokemonLocal> LoadPokeData(Stream fileStream) //todo :  
        {
            using var r = new StreamReader(fileStream);
            var json = r.ReadToEnd();

            var pkmns = JsonConvert.DeserializeObject<List<PokemonLocal>>(json);
            //ate aqui ok preenchemos nosso array com a lista do jasao e os argonautas
            for (var i = 0; i < pkmns.Count; i++)
            {
                if (pkmns[i].name.Contains("(")) //no mega/weirdos as for now
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