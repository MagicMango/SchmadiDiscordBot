using DiscordBotCore.Controller;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;


namespace DiscordBotCore.Commands
{
    public class WisdomCommand : IWillCommand
    {
        [Command("chucknorris"), Description("Get a randon Cuck Norris joke")]
        public async Task ChuckNorris(CommandContext ctx)
        {
            await ctx.RespondAsync(string.Format("Here is the Chuck Norris: Joke for {0}: {1}", ctx.User.Username, ApiController.GetChuckNorrisJoke()));
        }

        [Command("weather"), Description("Get the weather for a specified city")]
        public async Task Weather(CommandContext ctx, [Description("Name of the city")] string cityname)
        {
            await ctx.RespondAsync(string.Format("Here is the Weather for {0} requested from: {1}: {2}", cityname, ctx.User.Username, ApiController.GetWeatherForCity(cityname)));
        }

        [Command("joke"), Description("Get a randon joke")]
        public async Task Joke(CommandContext ctx)
        {
            await ctx.RespondAsync(string.Format("Here is the Joke for {0}: {1}", ctx.User.Username, ApiController.GetRandomJoke()));
        }

        [Command("rule34"), Description("Get a randon Rule 34 pic")]
        public async Task Rule34(CommandContext ctx)
        {
            await ctx.RespondAsync(string.Format("Here is your rule34 pic {0}: {1}", ctx.User.Username, ApiController.GetRandomRule34Link()));
        }

        [Command("giphy"), Description("Get a randon Giphy pic")]
        public async Task Giphy(CommandContext ctx)
        {
            await ctx.RespondAsync(string.Format("Here is your giphy pic {0}: {1}", ctx.User.Username, ApiController.GetRandomGiphyPic()));
        }
        
    }
}
