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
            new TypeRelations {Name = "Normal", Immunities = {"Ghost"}, Weaknesses = {"Rock", "Steel"}},
            new TypeRelations
            {
                Name = "Fire", Weaknesses = {"Fire", "Water", "Rock", "Dragon"},
                Strengths = {"Grass", "Ice", "Bug", "Steel"}
            },
            new TypeRelations
            {
                Name = "Water", Weaknesses = {"Water", "Grass", "Dragon"},
                Strengths = {"Fire", "Ground", "Rock"}
            },
            new TypeRelations
            {
                Name = "Electric", Immunities = {"Ground"}, Weaknesses = {"Electric", "Grass", "Dragon"},
                Strengths = {"Water", "Flying"}
            },
            new TypeRelations
            {
                Name = "Grass",
                Weaknesses = {"Fire", "Grass", "Poison", "Flying", "Bug", "Dragon", "Steel"},
                Strengths = {"Water", "Ground", "Rock"}
            },
            new TypeRelations
            {
                Name = "Ice", Weaknesses = {"Fire", "Water", "Ice", "Steel"},
                Strengths = {"Grass", "Ground", "Flying", "Dragon"}
            },
            new TypeRelations
            {
                Name = "Fighting", Immunities = {"Ghost"}, Weaknesses = {"Poison", "Flying", "Psychic", "Bug", "Fairy"},
                Strengths = {"Normal", "Ice", "Rock", "Dark", "Steel"}
            },
            new TypeRelations
            {
                Name = "Poison", Immunities = {"Steel"}, Weaknesses = {"Poison", "Ground", "Rock", "Ghost"},
                Strengths = {"Grass", "Fairy"}
            },
            new TypeRelations
            {
                Name = "Ground", Immunities = {"Flying"}, Weaknesses = {"Grass", "Bug"},
                Strengths = {"Fire", "Electric", "Poison", "Rock", "Steel"}
            },
            new TypeRelations
            {
                Name = "Flying", Weaknesses = {"Electric", "Rock", "Steel"},
                Strengths = {"Grass", "Fighting", "Bug"}
            },
            new TypeRelations
            {
                Name = "Psychic", Immunities = {"Dark"}, Weaknesses = {"Psychic", "Steel"},
                Strengths = {"Fighting", "Poison"}
            },
            new TypeRelations
            {
                Name = "Bug",
                Weaknesses = {"Fire", "Fighting", "Poison", "Flying", "Ghost", "Steel", "Fairy"},
                Strengths = {"Grass", "Psychic", "Dark"}
            },
            new TypeRelations
            {
                Name = "Rock", Weaknesses = {"Fighting", "Ground", "Steel"},
                Strengths = {"Fire", "Ice", "Flying", "Bug"}
            },
            new TypeRelations
                {Name = "Ghost", Immunities = {"Normal"}, Weaknesses = {"Dark"}, Strengths = {"Psychic", "Ghost"}},
            new TypeRelations {Name = "Dragon", Immunities = {"Fairy"}, Weaknesses = {"Steel"}, Strengths = {"Dragon"}},
            new TypeRelations
            {
                Name = "Dark", Weaknesses = {"Fighting", "Dark", "Fairy"},
                Strengths = {"Psychic", "Ghost"}
            },
            new TypeRelations
            {
                Name = "Steel", Weaknesses = {"Fire", "Water", "Electric", "Steel"},
                Strengths = {"Ice", "Rock", "Fairy"}
            },
            new TypeRelations
            {
                Name = "Fairy", Weaknesses = {"Fire", "Poison", "Steel"},
                Strengths = {"Fighting", "Dragon", "Dark"}
            }
        }.ToDictionary(i => i.Name);


        public static string GetWeakness(this PokemonLocal poke)
        {
            return TypeRelationships.GetValueOrDefault(poke.types[0])?.Weaknesses
                .GroupBy(i => i)
                .Select(i =>
                {
                    var items = i.ToList();
                    return items.Count == 1 ? i.Key : $"{i.Key}+{items.Count}";
                }).OrderBy(i => i).AggregateIfPossible((x, y) => $"{x}, {y}", "None");
        }

        public static string GetResistances(this PokemonLocal poke)
        {
            return TypeRelationships.GetValueOrDefault(poke.types[0])?.Strengths
                .GroupBy(i => i)
                .Select(i =>
                {
                    var items = i.ToList();
                    return items.Count == 1 ? i.Key : $"{i.Key}-{items.Count}";
                }).OrderBy(i => i).AggregateIfPossible((x, y) => $"{x}, {y}", "None");
        }

        public static string GetImmunities(this PokemonLocal poke)
        {
            return TypeRelationships.GetValueOrDefault(poke.types[0])?.Immunities
                .GroupBy(i => i)
                .Select(i =>
                {
                    var items = i.ToList();
                    return items.Count == 1 ? i.Key : $"{i.Key}+{items.Count}";
                }).OrderBy(i => i).AggregateIfPossible((x, y) => $"{x}, {y}", "None");
        }
    }
}