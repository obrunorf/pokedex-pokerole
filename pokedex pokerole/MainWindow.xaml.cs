using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Headers;
using PokeAPI;
using System.Windows.Media.Imaging;

namespace pokedex_pokerole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private List<move_complete> moves;
        private List<pokemon> pkmns; 
        public MainWindow()
        {
            InitializeComponent();           
            pkmns = LoadPokeData(@"C:\Users\e-eu\Documents\GitHub\pokedex-pokerole\pokedex pokerole\data\pokemon_finalzao.json");
            pkmnList.ItemsSource = pkmns;
            moves = LoadMoves(@"C:\Users\e-eu\Documents\GitHub\pokedex-pokerole\pokedex pokerole\data\moves.json");


        }

        private List<pokemon> LoadPokeData(string path)//todo :  nao adicionar pokemons mega
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();

                List<pokemon> pkmns = JsonConvert.DeserializeObject<List<pokemon>>(json);
                //ate aqui ok preenchemos nosso array com a lista do jasao e os argonautas
                for (var i = 0; i < pkmns.Count; i++)
                {
                    if (pkmns[i].name.Contains("("))
                    { pkmns.Remove(pkmns[i]); i--; };
                }

                return pkmns;
            }
        }

        private List<move_complete> LoadMoves(string path)//
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                List<move_complete> moves = JsonConvert.DeserializeObject<List<move_complete>>(json);
                return moves;
            }
        }

        private void LoadSingularPkmn(pokemon pkmn) //pkmns[number-1]
        {
            name.Text = pkmn.name;
            number.Text = pkmn.number;
            hieght.Text = pkmn.height+"m";
            weight.Text = pkmn.weight+"kg";
            category.Text = pkmn.category;
            stage1.Text = string.IsNullOrEmpty(pkmn.evolutions.ElementAtOrDefault(0)) ? pkmn.name : pkmn.evolutions[0]; ;
            stage2.Text = string.IsNullOrEmpty(pkmn.evolutions.ElementAtOrDefault(1)) ? "" :  pkmn.evolutions[1];
            stage3.Text = string.IsNullOrEmpty(pkmn.evolutions.ElementAtOrDefault(2)) ? "" :  pkmn.evolutions[2];
            str.Content = computeStats(pkmn.strength[0], pkmn.strength[1]);//pkmn.stat[a,b] is a vector in which a is base and b is limit
            dex.Content = computeStats(pkmn.dexterity[0], pkmn.dexterity[1]);
            vit.Content = computeStats(pkmn.vitality[0], pkmn.vitality[1]);
            spe.Content = computeStats(pkmn.special[0], pkmn.special[1]);
            ins.Content = computeStats(pkmn.insight[0], pkmn.insight[1]);
            disob.Content = computeStats(pkmn.disobedience, "5");
            Sprite(Int32.Parse(pkmn.number));
            about.Text = pkmn.pokedex;
            movesList.ItemsSource = pkmn.moves;
            if (movesList.Items.Count > 0)
            {
                movesList.ScrollIntoView(movesList.Items[0]);
            }
        }

        private void pkmnList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            foreach (pokemon poke in e.AddedItems)
            { 
              LoadSingularPkmn(poke); 
              clearMove(); }

        }

        private string computeStats(string baseStat, string limitStat)
        {
            if (string.IsNullOrEmpty(baseStat)){
                return "◯◯◯◯◯◯◯◯◯◯";
            }
            else {
                int baseStat2 = Int32.Parse(baseStat);
                int limitStat2 = Int32.Parse(limitStat);

                //lets take this slow, its 3am
                string composedStat = "";
                for (int i = 0; i < baseStat2; i++)
                {
                    composedStat += "⚫";
                }
                for (int j = 0; j < limitStat2 - baseStat2; j++)
                {
                    composedStat += "◯";
                }
                return composedStat; }
        }

        private async void  magicalThing()
        {
            //Pokemon p = await DataFetcher.GetApiObject<Pokemon>(1);            
            //Console.WriteLine(p.Name + " " + p.Stats[5].Stat.Name +" " + p.Stats[5].BaseValue);

            List<PokemonSpecies> all = new List<PokemonSpecies>();
            
            //foreach(pokemon po in pkmns)
            for (int a = 1; a <= 807; a++)  //1 ate 807          
             {
                    PokemonSpecies p2 = await DataFetcher.GetApiObject<PokemonSpecies>(a);
                    all.Add(p2);
                    Console.WriteLine("add " + p2.Name + " ..");
               
            }
            Console.WriteLine("all added.");
            Console.WriteLine(" hp.");
            //int i = 1;

            var todasEvolucaoMapeada = all.Select(x => x.EvolvesFromSpecies)
                                                   .Where(x => x?.Name != null)
                                                   .Distinct()
                                                   .ToLookup(x => x.Name.ToUpper());

            foreach (pokemon pok in pkmns) //buscar pelo nome! ignorar o q tiver entre parenteses!           
            {
                if (!string.IsNullOrEmpty(pok.number) && (Int32.Parse(pok.number)<808))
                //if (!string.IsNullOrEmpty(pok.name))
                {// { pok.number = i.ToString(); }
                    string pokemonnameprocessado = pok.name.Replace("'", "");
                    if (pok.name.IndexOf('(') >0) {
                        pokemonnameprocessado = (pok.name.Substring(0, pok.name.IndexOf('('))).Trim(); }    
                    if (pokemonnameprocessado.Contains("Rotom")) { pokemonnameprocessado = "Rotom"; }

                    string pokemonnameprocessado2 = pokemonnameprocessado;
                    if (pokemonnameprocessado.Contains("Tornadus")) { pokemonnameprocessado2 = "tornadus"; }
                    if (pokemonnameprocessado.Contains("Thundurus")) { pokemonnameprocessado2 = "thundurus"; }
                    if (pokemonnameprocessado.Contains("Landorus")) { pokemonnameprocessado2 = "landorus"; }
                    if (pokemonnameprocessado.Contains("Keldeo")) { pokemonnameprocessado2 = "keldeo"; }
                    if (pokemonnameprocessado.Contains("Meloetta")) { pokemonnameprocessado2 = "Meloetta"; }
                    if (pokemonnameprocessado.Contains("Oricorio")) { pokemonnameprocessado2 = "Oricorio"; }
                    if (pokemonnameprocessado.Contains("Wishiwashi")) { pokemonnameprocessado2 = "Wishiwashi"; }
                    if (pokemonnameprocessado.Contains("Minior")) { pokemonnameprocessado2 = "Minior"; }
                    if (pokemonnameprocessado.Contains("Mimikyu")) { pokemonnameprocessado2 = "Mimikyu"; }
                    if (pokemonnameprocessado.Contains("-Alola")) { pokemonnameprocessado2 = pokemonnameprocessado.Replace("-Alola",""); }
                    if (pokemonnameprocessado.Contains("-Galar")) { pokemonnameprocessado2 = pokemonnameprocessado.Replace("-Galar", ""); }
                    if (pokemonnameprocessado.Contains("Aegislash")) { pokemonnameprocessado2 = "Aegislash"; }
                    if (pokemonnameprocessado.Contains("Darmanitan")) { pokemonnameprocessado2 = "Darmanitan"; }

                    PokemonSpecies p2 = await DataFetcher.GetNamedApiObject<PokemonSpecies>(pokemonnameprocessado2.ToLower()); //ESPECIE

                    if (pokemonnameprocessado.Contains("Giratina")) { pokemonnameprocessado = "giratina-altered"; } //meu deus
                    if (pokemonnameprocessado.Contains("Basculin")) { pokemonnameprocessado = "basculin-red-striped"; }
                    if (pokemonnameprocessado.Contains("Darmanitan")) { pokemonnameprocessado = "darmanitan-standard"; }
                    if (pokemonnameprocessado.Contains("Meowstic")) { pokemonnameprocessado = "meowstic-male"; }
                    if (pokemonnameprocessado.Contains("-Galar")) { pokemonnameprocessado = pokemonnameprocessado.Replace("-Galar", ""); }
                    Pokemon p3 = await DataFetcher.GetNamedApiObject<Pokemon>(pokemonnameprocessado.ToLower());             //POKEMON

                    pok.abilities.Clear();
                    foreach (PokemonAbility abil in p3.Abilities)
                    {
                        if (abil.IsHidden) { pok.abilities.Add(abil.Ability.Name + "[HA]"); }
                        else { pok.abilities.Add(abil.Ability.Name); } 

                    };
                    
                    //Pokemon p3 = await DataFetcher.GetApiObject<Pokemon>(Int32.Parse(pok.number));

                    /*pok.base_hp = hpBase(pok,
                                    p2,
                                    todasEvolucaoMapeada,
                                    p3);
                    Console.WriteLine(pok.name + " com hp >" + pok.base_hp);

                    if (string.IsNullOrEmpty(pok.category))
                    {
                        pok.category = p2.Genera.FirstOrDefault(w => w.Language.Name == "en").Name;
                        //pok.category = p2.Genera[2].Name;
                        Console.WriteLine(pok.category);
                    }
                    if (string.IsNullOrEmpty(pok.pokedex))
                    {
                         pok.pokedex = p2.FlavorTexts[1].FlavorText;
                        pok.pokedex = p2.FlavorTexts.FirstOrDefault(w => w.Language.Name == "en").FlavorText;
                        Console.WriteLine(pok.pokedex);
                    }
                    if (string.IsNullOrEmpty(pok.height))
                    {
                        float temp = p3.Height /10 ;
                        pok.height = temp.ToString();
                        temp = p3.Mass / 10;
                        pok.weight = temp.ToString();
                    } */
                   // if (pok.types.Count == 0)
                   /* {
                        pok.types = new List<string>();
                        pok.types.Add(UppercaseFirst(p3.Types[0].Type.Name));
                        if (p3.Types.Count() > 1){ //se houver dois tipos
                            pok.types.Add(UppercaseFirst(p3.Types[1].Type.Name));
                        }
                    }*/
                    /*if (pok.evolutions.Count == 0){
                        //a cadeia evolucionaria é composta pelos q vieram antes, ele mesmo, e as evolucoes, em ordem
                        pok.evolutions = new List<string>();
                        if (!string.IsNullOrEmpty(p2.EvolvesFromSpecies?.Name ?? string.Empty))
                        {                            
                            pok.evolutions.Add(p2.EvolvesFromSpecies.Name);
                        }

                        pok.evolutions.Add(p2.Name);

                        EvolutionChain evo = await DataFetcher.GetApiObject<EvolutionChain>(p2.ID);                   
                        {
                            if (string.IsNullOrEmpty((evo.Chain.EvolvesTo.FirstOrDefault().Species?.Name ?? string.Empty))){
                                pok.evolutions.Add(evo.Chain.EvolvesTo.FirstOrDefault().Species.Name);
                            }

                            //  if (string.IsNullOrEmpty(evo.Chain.EvolvesTo.FirstOrDefault().EvolvesTo?.First().Species.Name ?? string.Empty)) //se houver outro estágio alem
                            // {
                            //pok.evolutions.Add(evo.Chain.EvolvesTo.FirstOrDefault().EvolvesTo.First().Species.Name);
                            // }

                        }                                              
                    }*/
                    //pok.defense =new List<string>();
                    //pok.spdef = new List<string>();                    

                    //pok.defense.AddRange(converterStatus(p3.Stats[2].BaseValue));
                    //pok.spdef.AddRange(converterStatus(p3.Stats[4].BaseValue));

                    /*if (pok.strength.Count == 0)
                    {
                        pok.strength = new List<string>();
                        pok.special = new List<string>();
                        pok.dexterity = new List<string>();
                        pok.strength.AddRange(converterStatus(p3.Stats[1].BaseValue));
                        pok.special.AddRange(converterStatus(p3.Stats[3].BaseValue));
                        pok.dexterity.AddRange(converterStatus(p3.Stats[5].BaseValue));
                    }*/

                }
                 //   i++;                
                
            }


            Console.WriteLine("os pokemon tudo feito");
            string json = JsonConvert.SerializeObject(pkmns.ToArray());

            //write string to file
            System.IO.File.WriteAllText(@"C:\Users\e-eu\Documents\GitHub\pokedex-pokerole\pokedex pokerole\data\POKEMONSREBUILTversao_meu_pau.json", json);

            //Console.WriteLine(p2.Name + " "+ p2.Names +" " + p2.PokedexNumbers);//species.EvolvesFromSpecies = null > is first form
        }

        private List<string> converterStatus(int original) //recebe o stat original, calcular o max a partir dai
        {
            int inicial, maximo;
            int original2 = ((original) * 2) + 5;

            switch (original2)
            {
                case int n when (n <= 20):
                    inicial = 1;   maximo = 1;
                    break;
                case int n when (n <= 44):
                    inicial = 1;  maximo = 2;
                    break;
                case int n when (n <= 94):
                    inicial = 1;  maximo = 3;
                    break;
                case int n when (n <= 144):
                    inicial = 2; maximo = 4;
                    break;
                case int n when (n <= 194):
                    inicial = 2; maximo = 5;
                    break;
                case int n when (n <= 244):
                    inicial = 3; maximo = 6;
                    break;
                case int n when (n <= 294):
                    inicial = 3; maximo = 7;
                    break;
                case int n when (n <= 344):
                    inicial = 4; maximo = 8;
                    break;
                case int n when (n <= 394):
                    inicial = 4; maximo = 9;
                    break;             
                default: //maior q 394 >> 5/10
                    inicial = 5; maximo = 10;
                    break;
            }
            var retList = new List<string>();
            retList.Add(inicial.ToString());
            retList.Add(maximo.ToString());
            return retList;
        }

        private string hpBase(pokemon pokeRole, PokemonSpecies pokeApi, ILookup<string, NamedApiResource<PokemonSpecies>> todasEvolucaoMapeada, Pokemon pokeApiPoke)
        /* The next was extracted from "Pokerole Ze stuff for later" on https://docs.google.com/document/d/180rP_Qc8MrPvNq99HZiBzVYvvqdwu60GnkIwh0papDU/edit
          As a note, Pokémon HP is a difficult thing to put to narrative meaning. How would you compare a 28 foot rock snake having its dreams eaten compared to a small fiery lizard getting squirted with a stream of bubbles? So, it might be easier to just stick to having Base HP based on the video games stats instead of coming up with a narrative reason to determine Base HP.

        The math behind the new tabletop stats; take the video game stats of a Pokémon and divide by 23.5, rounding down, with the following exceptions and guidelines:
        - Total HP
        Base HP + Vitality = Total HP
        Stat changes to Vitality, such as from moves, abilities, items, etc, do not affect total HP in any way.
        - Base HP
        Any Pokémon that can evolve has a minimum Base HP of 3.
        Any Pokémon that does not evolve has a minimum Base HP of 4.
        If a Pokémon evolves into another Pokémon with the same Base HP or less, the evolved Pokémon’s Base HP is one higher (Riolu and Lucario would both be 3, so Lucario is 4). This can stack with each evolution.
        Any Pokémon with Wonder Guard will have Base HP 1 and does not add Vitality to HP. This is an exception to all other rules and guidelines.
         */
        {   
            if (pokeRole.abilities.Contains("Wonder Guard")) { return "1"; } //exception

            int hpMin = 3;
            int hp = Convert.ToInt32(Math.Floor(pokeApiPoke.Stats[0].BaseValue / 5.0));            //stats is an array with the stats, base hp is [0] 23.5

           
            if (todasEvolucaoMapeada.Contains(pokeRole.name.ToUpper())) {// (Evolves(pokeRole, all)){
                hpMin = 3;
            } else hpMin = 4;
            if (pokeApi.EvolvesFromSpecies != null)
            {
                hpMin = Int32.Parse(pkmns.Find(p => p.name.ToUpper() == pokeApi.EvolvesFromSpecies.Name.ToUpper()).base_hp) +1; //if it has a previous evolution, it's minimum hp its the previous form +1
                hpMin = int.TryParse(pkmns.SingleOrDefault(p => p.name.ToUpper() == pokeApi.EvolvesFromSpecies.Name.ToUpper())?.base_hp, out var novoBrunao) ? novoBrunao+1 : hpMin;
            }
            //hpMin = 0;
            return Math.Max(hp,hpMin).ToString();            

        }

      /*  private bool Evolves(pokemon pokeRole, List<PokemonSpecies> all)
        {                 


            all.Find(p.)

            foreach(PokemonSpecies p in all)
            {
                if ((p.EvolvesFromSpecies?.Name ?? false) == pokeRole.name) return true; //it does evolve
            }
            return false;
        }*/

        private async void Sprite(int id)
        {
            Pokemon p = await DataFetcher.GetApiObject<Pokemon>(id);
            img.Source = new BitmapImage(new Uri(p.Sprites.FrontMale));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            magicalThing();
        }

        private void movesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Move mov in e.AddedItems)
            { LoadSingularMov(mov); }
        }

        private void LoadSingularMov(Move mov)
        {
            move_complete thunderbolt =  moves.Find(m => m.name.Equals(mov.name));
            moveName.Text = thunderbolt.name;
            moveDesc.Text = thunderbolt.description;
            moveAcc.Text = thunderbolt.accuracy;
            moveType.Text = thunderbolt.type;
            moveTarget.Text = thunderbolt.targets;
            moveEffect1.Text = thunderbolt.effect;
            moveDamage.Text = thunderbolt.damage_pool;
        }

        private void clearMove()
        {
            string yay = "???";
            moveName.Text = yay;
            moveDesc.Text = yay;
            moveAcc.Text = yay;
            moveType.Text = yay;
            moveTarget.Text = yay;
            moveEffect1.Text = yay;
            moveDamage.Text = yay;
        }

        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
    }
}
