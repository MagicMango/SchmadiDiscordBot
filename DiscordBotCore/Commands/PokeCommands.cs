using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBotCore.Repositorys;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DiscordBotCore.Commands
{
    public class PokeCommands : IWillCommand
    {
        [Command("poke"), Description("Guess that Pokemon!")]
        public async Task poke(CommandContext ctx) {

        }
    }
}
