using DiscordBotCore.Util;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DiscordBotCore.Controller
{
    public static class ApiController
    {

        public static string GetChuckNorrisJoke()
        {
            HttpClient client = new HttpClient();
            dynamic obj = null;
            string result = "";
            var t = Task.Run(async () =>
            {
                result = await client.GetStringAsync("http://api.icndb.com/jokes/random");
            });
            t.Wait();
            obj = JsonConvert.DeserializeObject<dynamic>(result);
            return (string)obj?.value?.joke;
        }

        public static string GetRandomJoke()
        {
            HttpClient client = new HttpClient();
            string result = "";
            client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var t = Task.Run(async () =>
            {
                result = await client.GetStringAsync("https://icanhazdadjoke.com/");
            });
            t.Wait();
            var obj = JsonConvert.DeserializeObject<dynamic>(result);
            return (string)obj?.joke;
        }

        public static string GetRandomGiphyPic()
        {
            HttpClient client = new HttpClient();
            string result = "";
            client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var t = Task.Run(async () =>
            {
                result = await client.GetStringAsync("https://api.giphy.com/v1/gifs/random?api_key=" + Configuration.GetStringSetting("discordbot:wisdomcommand:giphy:apikey") + "&tag=fail&rating=pg-13");
            });
            t.Wait();
            var obj = JsonConvert.DeserializeObject<dynamic>(result);
            return (string)obj?.data?.image_original_url;
        }

        public static string GetRandomRule34Link()
        {
            return GetFinalRedirect("https://rule34.xxx/index.php?page=post&s=random");
        }

        public static string GetWeatherForCity(string cityname)
        {
            HttpClient client = new HttpClient();
            string result = "";
            var t = Task.Run(async () =>
            {
                result = await client.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + cityname + "&APPID=" + Configuration.GetStringSetting("discordbot:wisdomcommand:weather:apikey") + "&units=" + Configuration.GetStringSetting("discordbot:wisdomcommand:weather:metric"));
            });
            t.Wait();
            var obj = JsonConvert.DeserializeObject<dynamic>(result);
            return "T: " + (string)obj?.main?.temp + " P: " + (string)obj?.main?.pressure + " H: " + (string)obj?.main?.humidity + " T(max): " + (string)obj?.main?.temp_max + " T(min): " + (string)obj?.main?.temp_min;
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
