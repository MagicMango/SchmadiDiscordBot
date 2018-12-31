using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace DiscordBotCore.Commands
{
    public class WisdomCommand : IWillCommand
    {
        [Command("chucknorris"), Description("Get a randon Cuck Norris joke")]
        public async Task ChuckNorris(CommandContext ctx)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync("http://api.icndb.com/jokes/random");
            var obj = JsonConvert.DeserializeObject<dynamic>(result);
            await ctx.RespondAsync(string.Format("Here is the Joke for {0}: {1}", ctx.User.Username, obj?.value?.joke));
        }
        [Command("weather"), Description("Get the weather for a specified city")]
        public async Task Weather(CommandContext ctx, [Description("Name of the city")] string cityname)
        {
            HttpClient client = new HttpClient();
            var result = await client.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + cityname + "&APPID=004b5e704d1ceb81a5d45fc5ff2f342b&units=metric");
            var obj = JsonConvert.DeserializeObject<dynamic>(result);
            await ctx.RespondAsync(string.Format("Here is the Weather for {0} requested from: {1}: {2}", cityname, ctx.User.Username, obj?.main));
        }
        [Command("joke"), Description("Get a randon joke")]
        public async Task Joke(CommandContext ctx)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.GetStringAsync("https://icanhazdadjoke.com/");
            var obj = JsonConvert.DeserializeObject<dynamic>(result);
            await ctx.RespondAsync(string.Format("Here is the Joke for {0}: {1}", ctx.User.Username, obj?.joke));
        }
        [Command("rule34"), Description("Get a randon Rule 34 pic")]
        public async Task Rule34(CommandContext ctx)
        {
            HttpClient client = new HttpClient();
            var result = GetFinalRedirect("https://rule34.xxx/index.php?page=post&s=random");
            await ctx.RespondAsync(string.Format("Here is your rule34 pic {0}: {1}", ctx.User.Username, result));
        }
        [Command("giphy"), Description("Get a randon Giphy pic")]
        public async Task Giphy(CommandContext ctx)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var result = await client.GetStringAsync("https://api.giphy.com/v1/gifs/random?api_key=dc6zaTOxFJmzC&tag=fail&rating=pg-13");
            var obj = JsonConvert.DeserializeObject<dynamic>(result);
            await ctx.RespondAsync(string.Format("Here is your giphy pic {0}: {1}", ctx.User.Username, obj?.data?.image_original_url));
        }
        public static string GetFinalRedirect(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return url;

            int maxRedirCount = 8;  // prevent infinite loops
            string newUrl = url;
            do
            {
                HttpWebRequest req = null;
                HttpWebResponse resp = null;
                try
                {
                    req = (HttpWebRequest)HttpWebRequest.Create(url);
                    req.Method = "HEAD";
                    req.AllowAutoRedirect = false;
                    resp = (HttpWebResponse)req.GetResponse();
                    switch (resp.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return newUrl;
                        case HttpStatusCode.Redirect:
                        case HttpStatusCode.MovedPermanently:
                        case HttpStatusCode.RedirectKeepVerb:
                        case HttpStatusCode.RedirectMethod:
                            newUrl = resp.Headers["Location"];
                            if (newUrl == null)
                                return url;

                            if (newUrl.IndexOf("://", System.StringComparison.Ordinal) == -1)
                            {
                                // Doesn't have a URL Schema, meaning it's a relative or absolute URL
                                Uri u = new Uri(new Uri(url), newUrl);
                                newUrl = u.ToString();
                            }
                            break;
                        default:
                            return newUrl;
                    }
                    url = newUrl;
                }
                catch (WebException)
                {
                    // Return the last known good URL
                    return newUrl;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (resp != null)
                        resp.Close();
                }
            } while (maxRedirCount-- > 0);

            return newUrl;
        }
    }
}
