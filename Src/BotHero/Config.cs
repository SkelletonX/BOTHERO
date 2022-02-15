using Microsoft.Extensions.Configuration;



namespace BotHero
{
    internal class Config
    {
        static string dstoken = System.Configuration.ConfigurationManager.AppSettings["Discord_Token"];

        public static string Token()
        {
            return dstoken;
        }
    }
}
