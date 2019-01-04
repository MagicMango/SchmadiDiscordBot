using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBotCore.Repositorys;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DiscordBotCore.Controller;

namespace DiscordBotCore.Commands
{
    public class PokeCommands : IWillCommand
    {
        [Command("poke"), Description("Guess a random Pokémon!")]
        public async Task poke(CommandContext ctx)
        {
            if (PokeController.IsGameStarted())
            {
                await ctx.RespondAsync(string.Format("Game already started!"));
            }
            else
            {
                await ctx.RespondAsync(string.Format("Guess that Pokémon: "));
                int pokeid = PokeController.GetRandomPokemonID();
                PokeController.SaveIDToFile(pokeid);
                await ctx.RespondWithFileAsync(PokeController.GetFileStream(pokeid));
            }

        }

        [Command("pokeguess"), Description("Command for guessing Pokémon.")]
        public async Task pokeguess(CommandContext ctx, [RemainingText, Description("Name of Pokémon.")] string pokename)
        {
            if (!PokeController.IsGameStarted())
            {
                await ctx.RespondAsync(string.Format("No current 'Guess That Pokémon' game started."));
            }
            else
            {
                if (PokeController.IsNameRight(pokename))
                {
                    await ctx.RespondAsync(string.Format("You {0} guessed right!", ctx.Member.Username));
                    PokeController.EndGame();
                }
                else {
                    await ctx.RespondAsync(string.Format("You {0} guessed wrong!", ctx.Member.Username));
                }
                                                            
            }
        }
    }
}
