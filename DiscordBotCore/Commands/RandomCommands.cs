using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DiscordBotCore;

namespace DiscordBotCore.Commands
{
    public class RandomCommands:IWillCommand
    {
        [Command("wsix"), Description("Joins a voice channel.")]
        public async Task WSix(CommandContext ctx)
        {
            Random r = new Random();
            int n = r.Next(1, 6);
            await ctx.RespondAsync($"You roled: " + n);
        }
    }
}
