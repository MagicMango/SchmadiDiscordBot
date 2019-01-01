using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;

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
                int w = r.Next(1, sided);
                await ctx.RespondAsync($"You roled: " + w);
            }
            
        }
    }
}
