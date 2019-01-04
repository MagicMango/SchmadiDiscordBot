using DiscordBotCore.Repositorys;
using DiscordBotCore.Util;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace DiscordBotCore.Controller
{
    public static class PokeController
    {
        public static string GetFileStream(int PokeID)
        {
            using (PokemonRepository repos = new PokemonRepository())
            {
                return AppDomain.CurrentDomain.BaseDirectory + @"App_Data" + repos.GetPokemonByID(PokeID).Pics;
            }
        }

        public static void SaveIDToFile(int PokeID) {

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"App_Data\CurrentPokemon", "" + PokeID);

        }

        public static int GetIDFromFile()
        {

            return int.Parse(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"App_Data\CurrentPokemon"));

        }

        public static bool IsGameStarted(){
            return File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"App_Data\CurrentPokemon");
        }

        public static void EndGame() {
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"App_Data\CurrentPokemon");      
        }

        public static int GetRandomPokemonID()
        {
            using (PokemonRepository repos = new PokemonRepository())
            {
                return repos.GetRandomPokemon();
            }
        }

        public static bool IsNameRight(string pokename)
        {
            using (PokemonRepository repos = new PokemonRepository())
            {
                return repos.GetIDFromName(pokename) == GetIDFromFile();
            }
        }
    }
}
