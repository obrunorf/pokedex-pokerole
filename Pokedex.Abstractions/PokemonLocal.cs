using System.Collections.Generic;

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
        public List<AbilityDetail> abilitiesDetailed { get; set; }
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
}