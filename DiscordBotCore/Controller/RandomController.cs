using DiscordBotCore.Util;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DiscordBotCore.Controller
{
    public static class RandomController
    {
        public static int[] GetRandomDice(int sided, int number) {
            Random r = new Random(DateTime.Now.Millisecond);
            int[] results = new int[number];
            for (int i = 0; i < number; i++)
            {
             results[i] = r.Next(1, sided); 
              
            }
            return results;
        }
    }
}
