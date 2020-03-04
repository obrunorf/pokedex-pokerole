using System.Collections.Generic;
using System.Linq;

namespace Pokedex.Abstractions
{
    public class PokemonLocal
    {
        public int? number { get; set; }
        public string name { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string category { get; set; }
        public string pokedex { get; set; }
        public List<string> evolutions { get; set; }
        public string evolution_method { get; set; }
        public string stage { get; set; }
        public string generation { get; set; }
        public List<string> types { get; set; }
        public List<int?> strength { get; set; }
        public List<int?> dexterity { get; set; }
        public List<int?> vitality { get; set; }
        public List<int?> special { get; set; }
        public List<int?> insight { get; set; }
        public List<string> abilities { get; set; }
        public List<AbilityDetail> abilitiesDetailed { get; set; } = new List<AbilityDetail>();
        public int? base_hp { get; set; }
        public int? disobedience { get; set; }
        public List<Move> moves { get; set; }
    }

    public class Move
    {
        public string name { get; set; }
        public string exp { get; set; }

        public MoveDetail Detail { get; set; }
    }

    public class MoveDetail
    {
        public string name { get; set; }
        public string type { get; set; }
        public string targets { get; set; }
        public string power { get; set; }
        public string category { get; set; }
        public string accuracy { get; set; }
        public string damage_pool { get; set; }
        public string effect { get; set; }
        public string description { get; set; }
    }

    public class AbilityDetail
    {
        public string name { get; set; }
        public string description { get; set; }
        public string effect { get; set; }        
    }

    public static class PokemonExtensions
    {
        public class TypeRelations
        {
            public string name { get; set; }
            public List<string> immunes { get; set; }
            public List<string> weaknesses { get; set; }
            public List<string> strengths { get; set; }
        }         

        public static List<TypeRelations> tipos = new List<TypeRelations>() {
            new TypeRelations() {name="Normal", immunes={"Ghost"}, weaknesses={"Rock","Steel"}, strengths={}},
            new TypeRelations() {name="Fire",immunes={},weaknesses={"Fire","Water","Rock","Dragon"},strengths={"Grass","Ice","Bug","Steel"}},
            new TypeRelations() {name="Water",immunes={},weaknesses={"Water","Grass","Dragon"},strengths={"Fire","Ground","Rock"}},
            new TypeRelations() {name="Electric",immunes={"Ground"},weaknesses={"Electric","Grass","Dragon"},strengths={"Water","Flying"}},
            new TypeRelations() {name="Grass",immunes={},weaknesses={"Fire","Grass","Poison","Flying","Bug","Dragon","Steel"},strengths={"Water","Ground","Rock"}},
            new TypeRelations() {name="Ice",immunes={},weaknesses={"Fire","Water","Ice","Steel"},strengths={"Grass","Ground","Flying","Dragon"}},
            new TypeRelations() {name="Fighting",immunes={"Ghost"},weaknesses={"Poison","Flying","Psychic","Bug","Fairy"},strengths={"Normal","Ice","Rock","Dark","Steel"}},
            new TypeRelations() {name="Poison",immunes={"Steel"},weaknesses={"Poison","Ground","Rock","Ghost"},strengths={"Grass","Fairy"}},
            new TypeRelations() {name="Ground",immunes={"Flying"},weaknesses={"Grass","Bug"},strengths={"Fire","Electric","Poison","Rock","Steel"}},
            new TypeRelations() {name="Flying",immunes={},weaknesses={"Electric","Rock","Steel"},strengths={"Grass","Fighting","Bug"}},
            new TypeRelations() {name="Psychic",immunes={"Dark"},weaknesses={"Psychic","Steel"},strengths={"Fighting","Poison"}},
            new TypeRelations() {name="Bug",immunes={},weaknesses={"Fire","Fighting","Poison","Flying","Ghost","Steel","Fairy"},strengths={"Grass","Psychic","Dark"}},
            new TypeRelations() {name="Rock",immunes={},weaknesses={"Fighting","Ground","Steel"},strengths={"Fire","Ice","Flying","Bug"}},
            new TypeRelations() {name="Ghost",immunes={"Normal"},weaknesses={"Dark"},strengths={"Psychic","Ghost"}},
            new TypeRelations() {name="Dragon",immunes={"Fairy"},weaknesses={"Steel"},strengths={"Dragon"}},
            new TypeRelations() {name="Dark",immunes={},weaknesses={"Fighting","Dark","Fairy"},strengths={"Psychic","Ghost"}},
            new TypeRelations() {name="Steel",immunes={},weaknesses={"Fire","Water","Electric","Steel"},strengths={"Ice","Rock","Fairy"}},
            new TypeRelations() {name="Fairy",immunes={},weaknesses={"Fire","Poison","Steel"},strengths={"Fighting","Dragon","Dark"}}
        };


        public static string GetWeakness(this PokemonLocal poke, string weakness)
        {
            IList<string> weakto = tipos.Find(o => o.name == poke.types[0]).weaknesses; //add all from type 1
            if (string.IsNullOrEmpty(poke.types[1])){                                   //and if there is a type 2, add all of them too
                foreach (string z in tipos.Find(o => o.name == poke.types[1]).weaknesses) {
                    weakto.Add(z);
                }
            }

            foreach(string w in weakto)
            {
                if (weakto.Where(x => x.Equals(w)).Count()>1)
                {
                    weakto.Add(w+"+2");
                    weakto.Remove(w);
                    weakto.Remove(w);
                }
            }

            return  string.Join(",", weakto);
        }

        public static string GetResistances(this PokemonLocal poke, string strengths)
        {
            IList<string> resistto = tipos.Find(o => o.name == poke.types[0]).strengths; //add all from type 1
            if (string.IsNullOrEmpty(poke.types[1]))
            {                                   //and if there is a type 2, add all of them too
                foreach (string z in tipos.Find(o => o.name == poke.types[1]).strengths)
                {
                    resistto.Add(z);
                }
            }

            foreach (string w in resistto)
            {
                if (resistto.Where(x => x.Equals(w)).Count() > 1)
                {
                    resistto.Add(w + "-2");
                    resistto.Remove(w);
                    resistto.Remove(w);
                }
            }

            return string.Join(",", resistto);
        }

        public static string GetImunities(this PokemonLocal poke, string immunes)
        {
            IList<string> immuneto = tipos.Find(o => o.name == poke.types[0]).immunes; //add all from type 1
            if (string.IsNullOrEmpty(poke.types[1]))
            {                                   //and if there is a type 2, add all of them too
                foreach (string z in tipos.Find(o => o.name == poke.types[1]).immunes)
                {
                    immuneto.Add(z);
                }
            }

            foreach (string w in immuneto)
            {
                if (immuneto.Where(x => x.Equals(w)).Count() > 1)
                {
                    immuneto.Remove(w);                    
                }
            }

            return string.Join(",", immuneto);
        }
    }
}