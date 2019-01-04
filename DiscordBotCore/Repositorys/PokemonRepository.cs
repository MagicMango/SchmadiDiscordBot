using DiscordBotCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotCore.Repositorys
{
    public class PokemonRepository:Repository
    {
        public int[] GetAllPokemon() {
            return context.PokeNames.Select(x => x.IDPokeNames).ToArray();
        }

        public int GetAllPokemonCount()
        {
            return context.PokeNames.Count();
        }

        public int GetRandomPokemon() {
            Random r = new Random(DateTime.Now.Millisecond);
            var rndm = r.Next(0, GetAllPokemonCount() - 1);
            return GetAllPokemon()[rndm];
        }
       

        public PokeNames GetPokemonByID(int Id) {
            return context.PokeNames.Find(Id);
        }

        public int GetIDFromName(string pokename) {
            return context.PokeNames
                .Where(x => x.NamesGerman.Equals(pokename) || x.NamesEnglish.Equals(pokename))
                .Select(y => y.IDPokeNames)
                .FirstOrDefault();
        }
    }
}
