using System;
using System.Linq;

namespace DiscordBotCore.Repositorys
{
    public class LovePhraseRepository: Repository
    {
        public string[] GetAllLovePhrases()
        {
            return context.LovePhrases.Select(x => x.Phrase).ToArray();
        }

        public int GetLovePhrasesCount()
        {
            return context.LovePhrases.Count();
        }

        public string GetRandomPhrase()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            var rn = r.Next(0, GetLovePhrasesCount() - 1);
            return GetAllLovePhrases()[rn];
        }
    }
}
