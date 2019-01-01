using DiscordBotCore.Repositorys;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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
                using (LovePhraseRepository repo = new LovePhraseRepository())
                {
                    var members = ctx.Guild.Members.Where(x => x.Id != ctx.User.Id && x.Id != ctx.Client.CurrentUser.Id);
                    if (members.Count() > 0)
                    {
                        Random r = new Random(DateTime.Now.Millisecond);
                        var rnu = r.Next(0, members.Count() - 1);
                        if ((members.ElementAt(rnu)?.Presence?.Status ?? UserStatus.Offline) == UserStatus.Offline)
                        {
                            await ctx.RespondAsync(string.Format(repo.GetRandomPhrase()+". But the User is offline ... you creepy bastard.", ctx.User.Username, members.ElementAt(rnu).Username));
                        }
                        else
                        {
                            await ctx.RespondAsync(string.Format(repo.GetRandomPhrase(), ctx.User.Username, members.ElementAt(rnu).Username));
                        }
                    }
                    else
                    {
                        await ctx.RespondAsync(string.Format("Hey {0} you know you are alone here right?", ctx.User.Username));
                    }
                }
            }
            catch (Exception e)
            {
                await ctx.RespondAsync("There is a problem with the database connection. Please fix me :(");
            }
        }
    }
}
