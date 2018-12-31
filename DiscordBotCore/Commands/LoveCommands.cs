using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DiscordBotCore.Commands
{
    public class LoveCommands : IWillCommand
    {
        private static string[] phrases = new string[] { "{0} spends some love for {1}", "{0} gives some love for {1}", "{0} kuddles {1}", "{0} gives {1} a big smooch" };


        [Command("love"), Description("Give love to someone!")]
        public async Task love(CommandContext ctx)
        {
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
    }
}
