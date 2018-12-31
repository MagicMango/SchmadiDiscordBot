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
        [Command("dice"), Description("Roles Dice x times")]
        public async Task Dice(CommandContext ctx, [Description("Side of dice.")] int sided, [Description("Number of dices")] int number)
        {
            for (int i = 0; i < number; i++)
            {
                Random r = new Random();
                int W = r.Next(1, sided);
                await ctx.RespondAsync($"You roled: " + W);
            }
            
        }
    }
}
