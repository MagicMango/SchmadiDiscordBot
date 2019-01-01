using DiscordBotCore.Entities;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotCore.Commands
{
    public class LoveCommands : IWillCommand
    {

        [Command("love"), Description("Give love to someone!")]
        public async Task love(CommandContext ctx)
        {
            try
            {
                using (DiscordBotEntities context = new DiscordBotEntities())
                {
                    var phrases = context.LovePhrases.Select(x => x.Phrase).ToArray();
                    var members = ctx.Guild.Members.Where(x => x.Id != ctx.User.Id && x.Id != ctx.Client.CurrentUser.Id);
                    if (members.Count() > 0)
                    {
                        Random r = new Random(DateTime.Now.Millisecond);
                        var rn = r.Next(0, phrases.Length - 1);
                        var rnu = r.Next(0, members.Count() - 1);
                        await ctx.RespondAsync(string.Format(phrases[rn], ctx.User.Username, members.ElementAt(rnu).Username));
                    }
                    else
                    {
                        await ctx.RespondAsync(string.Format("Hey {0} you know you are alone here right?", ctx.User.Username));
                    }
                }
            }catch(Exception e)
            {
                await ctx.RespondAsync("There is a problem with the database connection. Please fix me :(");
            }
        }
    }
}
