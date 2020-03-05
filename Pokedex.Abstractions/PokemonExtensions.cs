using System.Collections.Generic;
using System.Linq;

namespace Pokedex.Abstractions
{
    public static class PokemonExtensions
    {
        public class TypeRelations
        {
            public string Name { get; set; }
            public List<string> Immunities { get; } = new List<string>();
            public List<string> Weaknesses { get; } = new List<string>();
            public List<string> Strengths { get; } = new List<string>();
        }

        private static readonly Dictionary<string, TypeRelations> TypeRelationships = new List<TypeRelations>
        {
            new TypeRelations {Name = "Normal", Immunities = {"Ghost"}, Weaknesses = {"Fighting"}},
            new TypeRelations
            {
                Name = "Fire", Weaknesses = {"Ground", "Water", "Rock"},
                Strengths = {"Fire", "Ice", "Grass", "Bug","Steel", "Fairy"}
            },
            new TypeRelations
            {
                Name = "Water", Weaknesses = {"Electric", "Grass"},
                Strengths = {"Fire", "Water", "Ice", "Steel"}
            },
            new TypeRelations
            {
                Name = "Electric", Weaknesses = {"Ground"},
                Strengths = {"Steel", "Flying","Electric" }
            },
            new TypeRelations
            {
                Name = "Grass",
                Weaknesses = {"Fire", "Ice", "Poison", "Bug"},
                Strengths = {"Water", "Electric", "Grass","Ground"}
            },
            new TypeRelations
            {
                Name = "Ice", Weaknesses = {"Fire", "Fighting", "Rock", "Steel"},
                Strengths = {"Ice"}
            },
            new TypeRelations
            {
                Name = "Fighting", Weaknesses = {"Flying", "Psychic", "Fairy"},
                Strengths = { "Rock", "Dark", "Bug"}
            },
            new TypeRelations
            {
                Name = "Poison", Weaknesses = {"Psychic", "Ground"},
                Strengths = {"Grass", "Fairy","Fighting", "Poison", "Bug"}
            },
            new TypeRelations
            {
                Name = "Ground", Immunities = {"Electric"}, Weaknesses = {"Grass", "Ice", "Water"},
                Strengths = {"Poison", "Rock"}
            },
            new TypeRelations
            {
                Name = "Flying", Weaknesses = {"Electric", "Rock", "Ice"},
                Strengths = {"Grass", "Fighting", "Bug"},
                Immunities = {"Ground"}
            },
            new TypeRelations
            {
                Name = "Psychic", Weaknesses = {"Bug", "Ghost", "Dark"},
                Strengths = {"Fighting", "Psychic"}
            },
            new TypeRelations
            {
                Name = "Bug",
                Weaknesses = {"Fire","Flying", "Rock"},
                Strengths = {"Grass", "Fighting", "Ground"}
            },
            new TypeRelations
            {
                Name = "Rock", Weaknesses = {"Fighting", "Water", "Grass", "Ground", "Steel"},
                Strengths = {"Fire", "Normal", "Flying", "Poison"}
            },
            new TypeRelations
                {Name = "Ghost", Immunities = {"Normal, Fighting"}, Weaknesses = {"Dark, Ghost"}, Strengths = {"Poison", "Bug"}},

            new TypeRelations {Name = "Dragon", Weaknesses = {"Ice","Dragon","Fairy"}, Strengths = {"Fire","Water", "Electric","Grass"}},
            new TypeRelations
            {
                Name = "Dark",Immunities={ "Psychic"}, Weaknesses = {"Fighting", "Bug", "Fairy"},
                Strengths = {"Dark", "Ghost"}
            },
            new TypeRelations
            {
                Name = "Steel",Immunities={ "Poison"}, Weaknesses = {"Fire", "Fighting", "Ground"},
                Strengths = {"Normal", "Grass", "Ice", "Flying", "Psychic","Bug","Rock","Dragon","Steel","Fairy"}
            },
            new TypeRelations
            {
                Name = "Fairy",Immunities={ "Dragon"}, Weaknesses = {"Poison", "Steel"},
                Strengths = {"Fighting", "Bug", "Dark"}
            }
        }.ToDictionary(i => i.Name);


        public static string GetWeakness(this PokemonLocal poke)
        {
            return poke.types.SelectMany(i => TypeRelationships.GetValueOrDefault(i)?.Weaknesses ?? new List<string>())
                .GroupBy(i => i)
                .Select(i =>
                {
                    var items = i.ToList();
                    return items.Count == 1 ? i.Key : $"{i.Key}+{items.Count}";
                }).OrderBy(i => i).AggregateIfPossible((x, y) => $"{x}, {y}", "None");
        }

        public static string GetResistances(this PokemonLocal poke)
        {
            return poke.types.SelectMany(i => TypeRelationships.GetValueOrDefault(i)?.Strengths ?? new List<string>())
                .GroupBy(i => i)
                .Select(i =>
                {
                    var items = i.ToList();
                    return items.Count == 1 ? i.Key : $"{i.Key}-{items.Count}";
                }).OrderBy(i => i).AggregateIfPossible((x, y) => $"{x}, {y}", "None");
        }

        public static string GetImmunities(this PokemonLocal poke)
        {
            return poke.types.SelectMany(i => TypeRelationships.GetValueOrDefault(i)?.Immunities ?? new List<string>())
                .GroupBy(i => i)
                .Select(i =>
                {
                    var items = i.ToList();
                    return items.Count == 1 ? i.Key : $"{i.Key}+{items.Count}";
                }).OrderBy(i => i).AggregateIfPossible((x, y) => $"{x}, {y}", "None");
        }
    }
}