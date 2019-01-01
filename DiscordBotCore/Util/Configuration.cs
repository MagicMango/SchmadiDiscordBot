namespace DiscordBotCore.Util
{
    public static class Configuration
    {
        public static string GetStringSetting(string setting)
        {
            return System.Configuration.ConfigurationManager.AppSettings[setting];
        }

        public static int GetIntSetting(string setting)
        {
            return int.Parse(System.Configuration.ConfigurationManager.AppSettings[setting]);
        }

        public static bool GetBoolSetting(string setting)
        {
            return bool.Parse(System.Configuration.ConfigurationManager.AppSettings[setting]);
        }
    }
}
