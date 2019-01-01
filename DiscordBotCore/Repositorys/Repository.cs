using DiscordBotCore.Entities;
using System;

namespace DiscordBotCore.Repositorys
{
    public class Repository : IDisposable
    {
        protected DiscordBotEntities context;

        public Repository()
        {
            context = new DiscordBotEntities();
        }

        public void Dispose()
        {
            context?.Dispose();
        }
    }
}
