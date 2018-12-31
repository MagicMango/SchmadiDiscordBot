using DiscordBotCore.Handler;
using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelHandler h = new ChannelHandler();
            var t = h.GetBot;
            t.Wait();
            Console.ReadKey();
        }
    }
}
